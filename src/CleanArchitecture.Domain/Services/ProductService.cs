using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Domain.DTOs;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.InputModels;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces.Services;
using CleanArchitecture.Domain.Response;

namespace CleanArchitecture.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductListDTO>> GetAllProducts()
        {
            var result = await _productRepository.GetAll();
            var dto = result.Select(p => new ProductListDTO(p.Id, p.Name, p.Brand, p.Price, p.CreatedAt, p.UpdatedAt)).ToList();
            return dto;
        }

        public async Task<ProductListDTO> GetProductById(Guid id)
        {
            var result = await _productRepository.GetById(id);

            if (result is null)
            {
                throw new DomainNotFoundException("This product does not exist!");
            }

            return new ProductListDTO(
                result.Id,
                result.Name,
                result.Brand,
                result.Price,
                result.CreatedAt,
                result.UpdatedAt
            );
        }

        public async Task<GenericCommandResult> CreateProduct(ProductCreateInputModel productCreateInputModel)
        {
            try
            {
                var product = await _productRepository.Create(new Product(
                    productCreateInputModel.Name,
                    productCreateInputModel.Brand,
                    productCreateInputModel.Price,
                    DateTime.Now)
                );

                var dto = new ProductCreatedDTO(
                    product.Id,
                    product.Name,
                    product.Brand,
                    product.Price,
                    product.CreatedAt,
                    product.UpdatedAt
                );

                return new GenericCommandResult(true, "Produto Cadastrado com sucesso!", dto);
            }
            catch (Exception ex)
            {
                throw new DomainUnprocessableEntityException(ex.Message);
            }
        }

        public async Task<GenericCommandResult> DeleteProduct(Guid productId)
        {
            var product = await _productRepository.GetById(productId);

            if (product is null)
            {
                throw new DomainNotFoundException("This product does not exist!");
            }

            await _productRepository.Delete(product);
            return new GenericCommandResult(true, "Product was deleted successfully", product);
        }

        public async Task<GenericCommandResult> UpdateProduct(Guid id, ProductUpdateInputModel productUpdateInputModel)
        {
            var product = await _productRepository.GetById(id);

            if (product is null)
            {
                throw new DomainNotFoundException("This product does not exist!");
            }

            product.Name = productUpdateInputModel.Name;
            product.Brand = productUpdateInputModel.Brand;
            product.Price = productUpdateInputModel.Price;
            product.UpdatedAt = DateTime.Now;

            try
            {
                var result = await _productRepository.Update(product);
                return new GenericCommandResult(true, "Product was successfully updated!", result);
            }
            catch (Exception ex)
            {
                throw new DomainUnprocessableEntityException(ex.Message);
            }
        }
    }
}