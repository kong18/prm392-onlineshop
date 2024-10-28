using PRM392.OnlineStore.Domain.Entities.Models;

namespace PRM392.OnlineStore.Domain.Entities.Repositories
{
    namespace PRM392.OnlineStore.Domain.Entities.Repositories
    {
        public interface IChatMessageRepository : IEFRepository<ChatMessage, ChatMessage>
        {
            Task<IEnumerable<ChatMessage>> GetMessagesForUser(int userId, int recipientId, CancellationToken cancellationToken = default);
            Task AddMessage(ChatMessage chatMessage, CancellationToken cancellationToken = default);
        }
    }

}
