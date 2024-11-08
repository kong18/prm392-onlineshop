using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Infrastructure.Persistence;
using PRM392.OnlineStore.Infrastructure.Repositories;

public class ChatMessageRepository : RepositoryBase<ChatMessage, ChatMessage, ApplicationDbContext>, IChatMessageRepository
{
    public ChatMessageRepository(ApplicationDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    public async Task<IEnumerable<ChatMessage>> GetMessagesForUser(int userId, int? recipientId, int pageNumber = 1, int pageSize = 50, CancellationToken cancellationToken = default)
    {
        pageSize = pageSize > 50 ? 50 : pageSize;

        if (recipientId.HasValue)
        {
            return await FindAllAsync(
                cm => cm.UserId == userId && cm.RecipientId == recipientId.Value,
                //|| (cm.UserId == recipientId.Value && cm.RecipientId == userId),
                query => query
                    .OrderByDescending(cm => cm.SentAt)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize),
                cancellationToken
            );
        }
        else
        {
            var messages = await _dbContext.ChatMessages
            .Where(cm => cm.UserId == userId || cm.RecipientId == userId)
            .ToListAsync(cancellationToken);

            var latestMessages = messages
                .GroupBy(cm => new {
                    UserId = cm.UserId < cm.RecipientId ? cm.UserId : cm.RecipientId,
                    RecipientId = cm.UserId < cm.RecipientId ? cm.RecipientId : cm.UserId
                })
                .Select(g => g.OrderByDescending(cm => cm.SentAt).FirstOrDefault())
                .Where(m => m != null)
                .OrderByDescending(m => m.SentAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return latestMessages;
        }
    }
    public async Task<List<int>> GetRecipientsForUser(int userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.ChatMessages
            .Where(cm => cm.UserId == userId && cm.RecipientId.HasValue)
        .Select(cm => cm.RecipientId.Value)
        .Distinct()
        .ToListAsync(cancellationToken);
    }

    public async Task AddMessage(ChatMessage chatMessage, CancellationToken cancellationToken = default)
    {
        Add(chatMessage);
        await SaveChangesAsync(cancellationToken);
    }
}
