using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using PRM392.OnlineStore.Application.Common.DTO;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Domain.Entities.Repositories.PRM392.OnlineStore.Domain.Entities.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private static Dictionary<string, string> _userConnections = new Dictionary<string, string>();

    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatHub(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            _userConnections[userId] = Context.ConnectionId;

            var pendingMessages = await _chatMessageRepository.GetMessagesForUser(int.Parse(userId), -1, 1, 50);
            foreach (var message in pendingMessages)
            {
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", message.UserId, message);
            }
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        if (userId != null)
        {
            _userConnections.Remove(userId);
        }
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendMessage(ChatMessageDto message)
    {
        var userId = Context.UserIdentifier;
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
        {
            Console.WriteLine("Invalid sender user ID.");
            return;
        }
        if (message.RecipientId.HasValue && !string.IsNullOrEmpty(message.Message))
        {
            var chatMessage = new ChatMessage
            {
                UserId = int.Parse(userId),
                RecipientId = message.RecipientId.Value,
                Message = message.Message,
                SentAt = DateTime.UtcNow
            };

            try
            {
                await _chatMessageRepository.AddMessage(chatMessage, default);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving message to database: {ex.Message}");
                return;
            }

            if (_userConnections.TryGetValue(message.RecipientId.ToString(), out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", userId, message);
                await Clients.Caller.SendAsync("MessageReceived", "Message sent successfully!");
            }
        }
        else
        {
            Console.WriteLine("Invalid message or recipient.");
        }
    }
}
