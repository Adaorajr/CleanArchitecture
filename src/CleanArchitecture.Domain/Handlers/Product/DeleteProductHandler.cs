using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories;
using MediatR;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);

            if (product is null)
            {
                throw new DomainNotFoundException("This product does not exist!");
            }

            await _productRepository.Delete(product);
            return true;
        }
    }

}