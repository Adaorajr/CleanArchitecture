using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;
using System;
using CleanArchitecture.Domain.Response;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        public CreateProductHandler(IProductRepository productRepository, IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }
        public async Task<GenericCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
            {
                return new GenericCommandResult(false, "Please, check:", request.Notifications);
            }

            var product = await _productRepository.Create(new Entities.Product(
                request.Name,
                request.Brand,
                request.Price,
                DateTime.Now)
            );

            await _uow.Commit();

            CreateProductResponse response = product;
            return new GenericCommandResult(true, "Product successfully created!", response);
        }
    }
}