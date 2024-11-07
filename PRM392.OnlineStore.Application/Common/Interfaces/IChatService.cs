using PRM392.OnlineStore.Application.Common.DTO;

public interface IChatService
{
    Task SendMessageAsync(ChatMessageDto message);
    Task<List<RecipientInfo>> GetRecipientsForUserAsync(int userId);
    Task<List<ChatMessageDto>> GetMessagesAsync(int userId, int? recipientId, int pageNumber = 1, int pageSize = 50);
}
