using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using MediatR;
using System;
using System.Globalization;
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
            var product = await _uow.ProductRepository.Create(new Entities.Product(
                request.Name,
                request.Brand,
                Convert.ToDecimal(request.Price, new CultureInfo("pt-BR")),
                DateTime.Now)
            );

            await _uow.Commit();

            CreateProductResponse response = product;
            return new GenericCommandResult<CreateProductResponse>(true, "Product successfully created!", response);
        }
    }
}