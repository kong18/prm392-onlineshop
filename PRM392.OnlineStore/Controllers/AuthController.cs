using MediatR;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Interfaces;
using PRM392.OnlineStore.Application.Users.Authenticate;
using PRM392.OnlineStore.Application.Users;
using PRM392.OnlineStore.Domain.Entities.Repositories;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Application.Users.Register;
using PRM392.OnlineStore.Domain.Common.Exceptions;

namespace PRM392.OnlineStore.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IMediator _mediator;
        public AuthController(IUserRepository userRepository, IJwtService jwtService, IMediator mediator)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _mediator = mediator;
        }

        // Refresh token API to issue a new access token using the refresh token
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            var result = await _jwtService.RefreshTokenAsync(tokenRequest);

            if (result == null)
            {
                return Unauthorized("Invalid refresh token");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginQuery, cancellationToken);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(registerCommand, cancellationToken);
                return Ok(result); ;
            }
            catch (DuplicationException ex)
            {
                return BadRequest("Duplicate Account");
            }
            catch (ArgumentException ex)
            {
                return BadRequest("Password not matching");
            }


        }

    }
}
