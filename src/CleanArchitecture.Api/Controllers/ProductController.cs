using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.DTO.Product;
using CleanArchitecture.Domain.Interfaces.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ApiBaseController
    {
        private readonly IProductQueries _productQueries;
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;
        private const string PRODUCTS_KEY = "Products";
        public ProductController(IProductQueries productQueries
            , IMediator mediator
            , IMemoryCache memoryCache
            , INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _memoryCache = memoryCache;
            _mediator = mediator;
            _productQueries = productQueries;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get()
        {
            if (_memoryCache.TryGetValue(PRODUCTS_KEY, out List<ProductDTO> productsObject))
            {
                return ResponseGet(productsObject);
            }
            else
            {
                var products = await _productQueries.GetAllProducts();

                var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                    SlidingExpiration = TimeSpan.FromMinutes(20)
                };

                _memoryCache.Set(PRODUCTS_KEY, products, memoryCacheEntryOptions);

                return ResponseGet(products);
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ProducesResponseType(typeof(ProductDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            return ResponseGet(await _productQueries.GetProdutById(id));
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateProductResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CreateProductResponse>> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return ResponseError();

            var prod = result as GenericCommandResult<CreateProductResponse>;

            return ResponsePost(nameof(Get), new { id = prod.Data.Id }, prod.Data);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });

            if (!result.Success)
                return ResponseError();

            return ResponseDelete();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return ResponseError();

            return ResponsePutPatch();
        }
    }
}