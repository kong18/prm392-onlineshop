using MediatR;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Products.Create;
using static Google.Apis.Requests.BatchRequest;
using System.Net.Mime;

namespace PRM392.OnlineStore.Api.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _mediator;

        public ProductController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateProduct(
            [FromForm] CreateProductCommand command,
         CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }


    }
}
