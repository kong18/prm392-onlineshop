using PRM392.OnlineStore.Application.Common.DTO;

public interface IChatService
{
    Task SendMessageAsync(ChatMessageDto message);
    Task<List<ChatMessageDto>> GetMessagesAsync(int userId, int recipientId);
}
