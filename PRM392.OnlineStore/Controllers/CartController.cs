using MediatR;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Carts.Commands;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using System.Net.Mime;
using FluentValidation;
using PRM392.OnlineStore.Application.Carts.Queries;

namespace PRM392.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/carts")]
    public class CartController : ControllerBase
    {
        private readonly ISender _mediator;

        public CartController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateCart(
            [FromBody] CreateCartCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, "")); ;
            }
            catch (ValidationException ex)
            {
                var errorMessage = string.Join(", ", ex.Errors.Select(error => error.ErrorMessage));
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, errorMessage, "Validation errors occurred."));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, "")));
            }
        }

        [HttpPost("checkout")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CheckoutCart(
            [FromBody] CheckoutCartCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, "")); ;
            }
            catch (ValidationException ex)
            {
                var errorMessage = string.Join(", ", ex.Errors.Select(error => error.ErrorMessage));
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, errorMessage, "Validation errors occurred."));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, "")));
            }
        }

        [HttpDelete("{productId}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> RemoveCart(
            [FromRoute] int productId,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteCartCommand { ProductId = productId };
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, ""));
            }
            catch (ValidationException ex)
            {
                var errorMessage = string.Join(", ", ex.Errors.Select(error => error.ErrorMessage));
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, errorMessage, "Validation errors occurred."));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, "")));
            }
        }

        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateCart(
            [FromBody] UpdateCartCommand command,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                return Ok(new JsonResponse<string>(StatusCodes.Status200OK, result, ""));
            }
            catch (ValidationException ex)
            {
                var errorMessage = string.Join(", ", ex.Errors.Select(error => error.ErrorMessage));
                return BadRequest(new JsonResponse<string>(StatusCodes.Status400BadRequest, errorMessage, "Validation errors occurred."));
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, "")));
            }
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetCart(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(new GetCartByUserIdQuery(), cancellationToken);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new JsonResponse<string>(StatusCodes.Status404NotFound, ex.Message, ""));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new JsonResponse<string>(StatusCodes.Status401Unauthorized, ex.Message, ""));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, (new JsonResponse<string>(StatusCodes.Status500InternalServerError, ex.Message, "")));
            }
        }
    }
}
