using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Domain.Response;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, GenericCommandResult>
    {
        private readonly IUnitOfWorkContext _uow;
        public DeleteProductHandler(IUnitOfWorkContext uow)
        {
            _uow = uow;
        }

        public async Task<GenericCommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.ProductRepository.GetById(request.Id);

            if (product is null)
            {
                request.AddNotification("Error", "This product does not exist!");
                return new GenericCommandResult(false, "Please, check:", request.Notifications);
            }

            await _uow.ProductRepository.Delete(product);
            await _uow.Commit();

            return new GenericCommandResult(true, "Deleted successfully");
        }
    }

}