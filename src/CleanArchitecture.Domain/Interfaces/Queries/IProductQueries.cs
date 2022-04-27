using CleanArchitecture.Domain.DTO.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Queries
{
    public interface IProductQueries
    {
        Task<List<ProductDTO>> DapperTeste();
        Task<List<ProductDTO>> GetAllProducts();
        Task<ProductDTO> GetProdutById(Guid id);
    }
}
