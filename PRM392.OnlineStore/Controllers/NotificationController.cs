using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Common.Interfaces;

namespace PRM392.OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var response = await _notificationService.GetUserNotifications(userId);
            return Content(response, "application/json");
        }

        [HttpGet("user/{userId}/unread-count")]
        public async Task<IActionResult> GetUnreadNotificationCount(int userId)
        {
            var response = await _notificationService.GetUnreadNotificationCount(userId);
            return Content(response, "application/json");
        }

        [HttpPut("mark-as-read/{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
        {
            var response = await _notificationService.MarkNotificationAsRead(notificationId);
            return Content(response, "application/json");
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateNotification([FromQuery] int userId, [FromQuery] string message)
        {
            var response = await _notificationService.CreateNotification(userId, message);
            return Content(response, "application/json");
        }

        [HttpDelete("delete-old")]
        public async Task<IActionResult> DeleteNotifications([FromQuery] int id)
        {
            var response = await _notificationService.DeleteNotificationById(id);
            return Content(response, "application/json");
        }
    }
}
