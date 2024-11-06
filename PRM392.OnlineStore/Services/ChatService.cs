using AutoMapper;
using PRM392.OnlineStore.Application.Common.DTO;
using Microsoft.AspNetCore.SignalR;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Domain.Entities.Models;

namespace PRM392.OnlineStore.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatService(IChatMessageRepository chatMessageRepository, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }
        public ChatService(IChatMessageRepository chatMessageRepository, IMapper mapper)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
        }

        public async Task SendMessageAsync(ChatMessageDto messageDto)
        {
            var message = _mapper.Map<ChatMessage>(messageDto);
            await _chatMessageRepository.AddMessage(message);
            await _chatMessageRepository.UnitOfWork.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", messageDto);
        }

        public async Task<List<ChatMessageDto>> GetMessagesAsync(int userId, int recipientId)
        {
            var messages = await _chatMessageRepository.GetMessagesForUser(userId, recipientId);
            return _mapper.Map<List<ChatMessageDto>>(messages);
        }
    }
}
