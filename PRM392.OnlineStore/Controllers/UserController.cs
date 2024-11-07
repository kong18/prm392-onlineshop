using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Users.GetInfo;

namespace PRM392.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/user/info
        [HttpGet("info")]
        [Authorize] // Ensure only authenticated users can access this endpoint
        public async Task<IActionResult> GetUserInfo()
        {
            var result = await _mediator.Send(new GetUserInfoQuery());
            if (result == null)
            {
                return NotFound("User not found");
            }
            return Ok(result);
        }
    }
}
