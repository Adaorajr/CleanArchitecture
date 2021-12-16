using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.InputModels;
using CleanArchitecture.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();

            if (result.Count > 0)
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
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