using AutoMapper;
using PRM392.OnlineStore.Application.Common.DTO;
//using Microsoft.AspNetCore.SignalR;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Domain.Entities.Models;
using Microsoft.EntityFrameworkCore;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using Firebase.Database;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Firebase.Database.Query;

namespace PRM392.OnlineStore.Api.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly FirebaseClient _firebaseClient;

        public ChatService(IChatMessageRepository chatMessageRepository, IMapper mapper, IUserRepository userRepository, FirebaseClient firebaseClient)
        {
            _chatMessageRepository = chatMessageRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _firebaseClient = firebaseClient;
        }

        public async Task SendMessageAsync(ChatMessageDto messageDto)
        {
            var message = new ChatMessage
            {
                UserId = messageDto.UserId,
                RecipientId = messageDto.RecipientId,
                Message = messageDto.Message,
                SentAt = DateTime.UtcNow
            };
            await _chatMessageRepository.AddMessage(message);
            await _chatMessageRepository.UnitOfWork.SaveChangesAsync();

            try
            {
                var firebaseMessage = new
                {
                    UserId = messageDto.UserId,
                    RecipientId = messageDto.RecipientId,
                    Message = messageDto.Message,
                    SentAt = DateTime.UtcNow
                };

                await _firebaseClient
                    .Child("chat_messages")
                    .Child($"{messageDto.UserId}_{messageDto.RecipientId}")
                    .PostAsync(firebaseMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error posting to Firebase: " + ex.Message);
                throw;
            }
        }

        public async Task<List<ChatMessageDto>> GetMessagesAsync(int userId, int? recipientId)
        {
            var messages = await _chatMessageRepository.GetMessagesForUser(userId, recipientId);
            return messages.Select(m => new ChatMessageDto
            {
                UserId = m.UserId,
                RecipientId = m.RecipientId,
                Message = m.Message,
                SentAt = m.SentAt
            }).ToList();
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
