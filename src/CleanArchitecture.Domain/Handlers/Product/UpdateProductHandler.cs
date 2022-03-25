using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Response;
using MediatR;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<GenericCommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new GenericCommandResult(false, "Check, please:", request.Notifications);

            var product = await _productRepository.GetById(request.Id);

            if (product is null)
            {
                request.AddNotification("Error", "This product does not exist!");
                return new GenericCommandResult(false, "Please, check:", request.Notifications);
            }

            product.Name = request.Name;
            product.Brand = request.Brand;
            product.Price = request.Price;
            product.UpdatedAt = DateTime.Now;

            try
            {
                var result = await _productRepository.Update(product);
                return new GenericCommandResult(true, "Product successfully updated!", result);
            }
            catch (Exception ex)
            {
                throw new DomainUnprocessableEntityException(ex.Message);
            }
        }
    }
}