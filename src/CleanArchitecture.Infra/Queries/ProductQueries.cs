using CleanArchitecture.Domain.DTO.Product;
using CleanArchitecture.Domain.Interfaces.Queries;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Queries
{
    public class ProductQueries : IProductQueries
    {
        private readonly IProductRepository _productRepository;

        public ProductQueries(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            using var dapper = _productRepository.GetConnection();

            const string sql = "SELECT p.Id, p.Name, p.Brand, p.Price, p.CreatedAt, p.UpdatedAt FROM Products p";
            return (await dapper.QueryAsync<ProductDTO>(sql)).ToList();
        }

        public async Task<ProductDTO> GetProdutById(int id)
        {
            var result = await _productRepository.GetById(id);

            if (result is null)
                return null;

            var response = new ProductDTO(
                            result.Id.ToString(),
                            result.Name,
                            result.Brand,
                            result.Price,
                            result.CreatedAt.ToString(),
                            result.UpdatedAt.ToString()
                        );

            return response;
        }
    }
}
