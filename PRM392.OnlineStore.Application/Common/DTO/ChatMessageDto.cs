using AutoMapper;
using PRM392.OnlineStore.Application.Common.Mappings;
using PRM392.OnlineStore.Domain.Entities.Models;

namespace PRM392.OnlineStore.Application.Common.DTO
{
    public class ChatMessageDto : IMapFrom<ChatMessage>
    {
        public int? UserId { get; set; }
        public int? RecipientId { get; set; }
        public string? Message { get; set; }
        public DateTime SentAt { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChatMessageDto, ChatMessage>();
        }
    }
}
