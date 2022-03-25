using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Response;
using MediatR;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, GenericCommandResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _uow;
        public DeleteProductHandler(IProductRepository productRepository, IUnitOfWork uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }

        public async Task<GenericCommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(request.Id);

            if (product is null)
            {
                request.AddNotification("Error", "This product does not exist!");
                return new GenericCommandResult(false, "Please, check:", request.Notifications);
            }

            await _productRepository.Delete(product);
            await _uow.Commit();

            return new GenericCommandResult(true, "Deleted successfully");
        }
    }

}