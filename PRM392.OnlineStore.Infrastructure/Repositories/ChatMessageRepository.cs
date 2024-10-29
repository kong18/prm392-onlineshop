using AutoMapper;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using PRM392.OnlineStore.Infrastructure.Repositories;

public class ChatMessageRepository : RepositoryBase<ChatMessage, ChatMessage, ApplicationDbContext>, IChatMessageRepository
{
    public ChatMessageRepository(ApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<IEnumerable<ChatMessage>> GetMessagesForUser(int userId, int recipientId, CancellationToken cancellationToken = default)
    {
        return await FindAllAsync(cm => cm.UserId == userId, cancellationToken);
    }

    public async Task AddMessage(ChatMessage chatMessage, CancellationToken cancellationToken = default)
    {
        Add(chatMessage);
        await SaveChangesAsync(cancellationToken);
    }
}
