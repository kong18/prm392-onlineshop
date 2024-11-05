using Microsoft.AspNetCore.SignalR;
using PRM392.OnlineStore.Application.Common.DTO;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private static Dictionary<string, string> _userConnections = new Dictionary<string, string>();

    public override Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections[userId] = Context.ConnectionId;
        }
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var userId = Context.UserIdentifier;
        if (!string.IsNullOrEmpty(userId))
        {
            _userConnections.Remove(userId);
        }
        return base.OnDisconnectedAsync(exception);
    }
    public async Task SendMessage(ChatMessageDto message)
    {
        //await Clients.All.SendAsync("ReceiveMessage", message);
        if (message.RecipientId.HasValue && _userConnections.TryGetValue(message.RecipientId.ToString(), out var connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "User is not connected.");
        }
    }
}
