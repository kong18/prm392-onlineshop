using MediatR;
using Microsoft.AspNetCore.Mvc;
using PRM392.OnlineStore.Application.Products.Create;
using static Google.Apis.Requests.BatchRequest;
using System.Net.Mime;
using PRM392.OnlineStore.Application.Products.GetById;
using PRM392.OnlineStore.Application.Products;
using PRM392.OnlineStore.Domain.Entities.Models;
using PRM392.OnlineStore.Application.Pagination;
using PRM392.OnlineStore.Application.Products.Filter;
using PRM392.OnlineStore.Domain.Common.Exceptions;
using PRM392.OnlineStore.Application.Products.Update;
using PRM392.OnlineStore.Application.Products.Delete;

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
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDto>> GetProduct(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
            return Ok((result));
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(JsonResponse<List<Product>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<JsonResponse<List<Product>>>> FilterProduct(
        [FromQuery] FilterProductQuery query,
        CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query, cancellationToken);

            var response = new JsonResponse<List<ProductDto>>(
                data: result.Data.ToList()  
            )
            {
                TotalCount = result.TotalCount,
                PageNumber = result.PageNumber,
                TotalPages = result.TotalPage,
                PageCount = result.PageCount,
                PageSize = result.PageSize
            };

            return Ok(response);
        }



        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateProduct(
            [FromForm] UpdateProductCommand command,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

        [HttpDelete("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteProduct(
            [FromRoute] int id,
            CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return Ok(new JsonResponse<string>(result));
        }

    }
}
