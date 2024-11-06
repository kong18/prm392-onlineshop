using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using PRM392.OnlineStore.Application.PayOs;

namespace PRM392.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/payos")]
    public class PayOsController : ControllerBase
    {
        private readonly PayOsServices _payOsServices;

        public PayOsController(PayOsServices payOsServices)
        {
            _payOsServices = payOsServices;
        }
        [HttpPost("hook")]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookType webhookBody)
        {
            try
            {
                var result = await _payOsServices.ProcessPaymentResponse(webhookBody);

                if (result == "Payment success")
                {
                    return Ok(new { Message = "Webhook processed successfully"});
                }

                return BadRequest(new { Message = "Webhook processing failed."});

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
