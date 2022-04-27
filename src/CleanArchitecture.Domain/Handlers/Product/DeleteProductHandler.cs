using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Handlers.Notifications;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, GenericCommandResult>
    {
        private readonly IUnitOfWorkContext _uow;
        private readonly IDomainNotificationMediatorService _domainNotification;
        public DeleteProductHandler(IUnitOfWorkContext uow, IDomainNotificationMediatorService domainNotification)
        {
            _uow = uow;
            _domainNotification = domainNotification;
        }

        public async Task<GenericCommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.ProductRepository.GetById(request.Id);
       
            if (product is null)
            {
                _domainNotification.Notify(new Commons.DomainNotification("Error", "This product does not exist!"));
                return new GenericCommandResult(false, "Invalid Request!");
            }

            await _uow.ProductRepository.Delete(product);
            await _uow.Commit();

            return new GenericCommandResult(true, "Deleted successfully");
        }
    }

}