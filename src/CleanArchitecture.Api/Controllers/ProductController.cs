using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.InputModels;
using CleanArchitecture.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMemoryCache _memoryCache;
        private const string PRODUCTS_KEY = "Products";
        public ProductController(IProductService productService, IMemoryCache memoryCache)
        {
            _productService = productService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        [Route("Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            if (_memoryCache.TryGetValue(PRODUCTS_KEY, out object productsObject))
            {
                return Ok(productsObject);
            }
            else
            {
                var products = await _productService.GetAllProducts();

                if (products.Count > 0)
                {
                    var memoryCacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
                        SlidingExpiration = TimeSpan.FromMinutes(20)
                    };

                    _memoryCache.Set(PRODUCTS_KEY, products, memoryCacheEntryOptions);

                    return Ok(products);
                }
                else
                {
                    return NoContent();
                }
            }
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateInputModel productCreateInputModel)
        {
            var result = await _productService.CreateProduct(productCreateInputModel);
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] Guid productId)
        {
            var result = await _productService.DeleteProduct(productId);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromQuery] Guid id, [FromBody] ProductUpdateInputModel productUpdateInputModel)
        {
            var result = await _productService.UpdateProduct(id, productUpdateInputModel);

            return Ok(result);
        }
    }
}