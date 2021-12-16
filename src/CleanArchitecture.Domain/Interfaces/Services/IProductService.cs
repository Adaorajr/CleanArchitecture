using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Domain.DTOs;
using CleanArchitecture.Domain.InputModels;
using CleanArchitecture.Domain.Response;

namespace CleanArchitecture.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<List<ProductListDTO>> GetAllProducts();
        Task<ProductListDTO> GetProductById(Guid id);
        Task<GenericCommandResult> CreateProduct(ProductCreateInputModel productCreateInputModel);
        Task<GenericCommandResult> DeleteProduct(Guid productId);
        Task<GenericCommandResult> UpdateProduct(Guid id, ProductUpdateInputModel productUpdateInputModel);
    }
}