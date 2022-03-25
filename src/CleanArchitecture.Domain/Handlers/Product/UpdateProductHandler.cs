using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);

            if (product is null)
            {
                throw new DomainNotFoundException("This product does not exist!");
            }

            product.Name = request.Name;
            product.Brand = request.Brand;
            product.Price = request.Price;
            product.UpdatedAt = DateTime.Now;

            try
            {
                var result = await _productRepository.Update(product);
                return true;
            }
            catch (Exception ex)
            {
                throw new DomainUnprocessableEntityException(ex.Message);
            }
        }
    }

}