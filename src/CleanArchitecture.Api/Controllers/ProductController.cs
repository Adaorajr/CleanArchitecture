using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.DTOs;
using CleanArchitecture.Domain.InputModels;
using CleanArchitecture.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.JsonPatch;
using CleanArchitecture.Api.Extensions;

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
        public async Task<IActionResult> Get()
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

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateInputModel productCreateInputModel)
        {
            var result = await _productService.CreateProduct(productCreateInputModel);
            var dto = result.Data as ProductCreatedDTO;

            return CreatedAtAction(nameof(Get), new { id = dto.Id }, dto);
        }

        [HttpPost]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdateInputModel productUpdateInputModel)
        {
            var result = await _productService.UpdateProduct(id, productUpdateInputModel);

            return Ok(result);
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<ProductUpdateInputModel> productUpdateInputModel)
        {
            var result = await _productService.PatchProduct(id, productUpdateInputModel);

            return Ok(result);
        }
    }
}