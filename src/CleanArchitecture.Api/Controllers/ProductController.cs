using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MediatR;
using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Queries.Requests.Product;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private const string PRODUCTS_KEY = "Products";
        public ProductController(IMediator mediator, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (_memoryCache.TryGetValue(PRODUCTS_KEY, out object productsObject))
            {
                return Ok(productsObject);
            }
            else
            {
                var products = await _mediator.Send(new GetAllProductsQuery());

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                    SlidingExpiration = TimeSpan.FromMinutes(20)
                };

                _memoryCache.Set(PRODUCTS_KEY, products, memoryCacheEntryOptions);

                return Ok(products);
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery { Id = id });

            if (!result.Success)
                return NotFound(result);
            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);

            var prod = result.Data as CreateProductResponse;

            return CreatedAtAction(nameof(Get), new { id = prod.Id }, prod);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });

            if (!result.Success)
                return BadRequest(result);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result);

            return NoContent();
        }
    }
}