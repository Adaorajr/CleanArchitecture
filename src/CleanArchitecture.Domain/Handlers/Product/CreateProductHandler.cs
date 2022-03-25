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
        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<GenericCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
            {
                return new GenericCommandResult(false, "Verifique os erros!", request.Notifications);
            }

            var product = await _productRepository.Create(new Entities.Product(
                request.Name,
                request.Brand,
                request.Price,
                DateTime.Now)
            );

            CreateProductResponse response = product;


            return new GenericCommandResult(true, "Produto Cadastrado com Sucesso!", response);
        }
    }
}