using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using System;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.Create(new Entities.Product(
            request.Name,
            request.Brand,
            request.Price,
            DateTime.Now)
            );

            return new CreateProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Name,
                Price = product.Price,
                CreatedAt = product.CreatedAt,
                UpdatedAt = null
            };
        }
    }
}