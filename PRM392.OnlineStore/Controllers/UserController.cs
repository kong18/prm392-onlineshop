using MediatR;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Users.GetInfo;
using PRM392.OnlineStore.Application.Users;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using System.Net.Mime;

namespace PRM392.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly ISender _mediator;

        public UserController(ISender mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("info")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetUserInfo(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetUserInfo(), cancellationToken);
                return Ok(new JsonResponse<UserDto>(StatusCodes.Status200OK, "Get Success", result));
            }
            catch (NotFoundException ex)
            {
                return NotFound(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, ""));
            }
        }
    }
}
