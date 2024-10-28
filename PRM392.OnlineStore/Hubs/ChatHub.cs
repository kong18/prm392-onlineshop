using Microsoft.AspNetCore.SignalR;
using PRM392.OnlineStore.Application.Common.DTO;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task SendMessage(ChatMessageDto message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
}
