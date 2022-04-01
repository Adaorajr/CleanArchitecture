using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Domain.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, GenericCommandResult>
    {
        private readonly IUnitOfWorkContext _uow;
        public CreateProductHandler(IUnitOfWorkContext uow)
        {
            _uow = uow;
        }
        public async Task<GenericCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
            {
                return new GenericCommandResult(false, "Please, check:", request.Notifications);
            }

            var product = await _uow.ProductRepository.Create(new Entities.Product(
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