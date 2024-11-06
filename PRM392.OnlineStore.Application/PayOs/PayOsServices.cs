using Net.payOS;
using Net.payOS.Types;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.PayOs
{
    public class PayOsServices
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly PayOS _payOs;
        private readonly IOrderRepository _orderRepository;
        public PayOsServices(ICurrentUserService currentUserService, IPaymentRepository paymentRepository, IOrderRepository orderRepository, PayOS payOS)
        {
            _currentUserService = currentUserService;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _payOs = payOS;
        }
        public async Task<string> CreatePaymentLink(PaymentRequest model)
        {
            if (_currentUserService.UserId == null)
            {
                throw new UnauthorizedAccessException("User not logged in");
            }

            long orderCode = model.OrderId;
            long expiredAt = (long)(DateTime.UtcNow.AddMinutes(10) - new DateTime(1970, 1, 1)).TotalSeconds;

            ItemData item = new ItemData("Mì tôm hảo hảo ly", 1, 1000);
            List<ItemData> items = new List<ItemData> { item };

            PaymentData paymentData = new PaymentData(orderCode, (int)model.Amount, $"Pay for {model.Amount / 100}", items, "cancelUrl", "returnUrl");
            CreatePaymentResult createPaymentResult = await _payOs.createPaymentLink(paymentData);

            // Insert new payment record into Payments table
            var newPayment = new Payment
            {
                OrderId = model.OrderId,
                Amount = model.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = "Pending" // Initial status
            };
            _paymentRepository.Add(newPayment);
            await _paymentRepository.UnitOfWork.SaveChangesAsync();

            return createPaymentResult.checkoutUrl;
        }

        public async Task<string> ProcessPaymentResponse(WebhookType webhookBody)
        {
            WebhookData verifiedData = _payOs.verifyPaymentWebhookData(webhookBody);
            string responseCode = verifiedData.code;
            string orderCode = verifiedData.orderCode.ToString();

            // Find payment record by OrderID
            var payment = await _paymentRepository.FindAsync(x => x.OrderId == int.Parse(orderCode));

            if (payment != null)
            {
                if (responseCode == "00") // Success code
                {
                    payment.PaymentStatus = "Successful";
                    _paymentRepository.Update(payment);
                    await _paymentRepository.UnitOfWork.SaveChangesAsync();

                    var existOrder = await _orderRepository.FindAsync(x => x.OrderId == payment.OrderId);
                    if (existOrder != null)
                    {
                        existOrder.OrderStatus = "Success";
                        _orderRepository.Update(existOrder);
                        await _orderRepository.UnitOfWork.SaveChangesAsync();
                        return "Payment success";
                    }
                }
                else
                {
                    payment.PaymentStatus = "Failed";
                    _paymentRepository.Update(payment);
                    await _paymentRepository.UnitOfWork.SaveChangesAsync();
                }
            }

            return "Payment failed";
        }
    }
}
