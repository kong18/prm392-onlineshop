using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Common.DTO;

[Route("api/chat")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto message)
    {
        await _chatService.SendMessageAsync(message);
        return Ok();
    }

    [HttpGet("messages/{userId}/{recipientId}")]
    public async Task<ActionResult<List<ChatMessageDto>>> GetMessages(int userId, int recipientId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 50)
    {
        var messages = await _chatService.GetMessagesAsync(userId, recipientId, pageNumber, pageSize);
        return Ok(messages);
    }
}
