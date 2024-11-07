using AutoMapper;
using PRM392.OnlineStore.Application.Common.DTO;
using Microsoft.AspNetCore.SignalR;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using PRM392.OnlineStore.Domain.Entities.Repositories;

namespace PRM392.OnlineStore.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IUserRepository _userRepository;

        public ChatService(IChatMessageRepository chatMessageRepository, IMapper mapper, IHubContext<ChatHub> hubContext, IUserRepository userRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _userRepository = userRepository;
        }

        public async Task SendMessageAsync(ChatMessageDto messageDto)
        {
            var message = _mapper.Map<ChatMessage>(messageDto);

            await _chatMessageRepository.AddMessage(message);
            await _chatMessageRepository.UnitOfWork.SaveChangesAsync();

            if (messageDto.RecipientId.HasValue)
            {
                await _hubContext.Clients.User(messageDto.RecipientId.ToString())
                    .SendAsync("ReceiveMessage", messageDto);
            }
        }
        public async Task<List<RecipientInfo>> GetRecipientsForUserAsync(int userId)
        {
            var recipientIds = await _chatMessageRepository.GetRecipientsForUser(userId);

            var recipients = await _userRepository.GetUsersByIdsAsync(recipientIds);

            var recipientInfo = recipients.Select(u => new RecipientInfo
            {
                RecipientId = u.UserId,
                Username = u.Username
            }).ToList();

            return recipientInfo;
        }

        public async Task<List<ChatMessageDto>> GetMessagesAsync(int userId, int? recipientId, int pageNumber = 1, int pageSize = 50)
        {
            var messages = await _chatMessageRepository.GetMessagesForUser(userId, recipientId, pageNumber, pageSize);
            return _mapper.Map<List<ChatMessageDto>>(messages);
        }
    }
}
