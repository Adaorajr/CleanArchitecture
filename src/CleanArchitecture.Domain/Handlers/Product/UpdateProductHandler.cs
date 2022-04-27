using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Handlers.Notifications;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, GenericCommandResult>
    {
        private readonly IUnitOfWorkContext _uow;
        private readonly IDomainNotificationMediatorService _domainNotification;
        public UpdateProductHandler(IUnitOfWorkContext uow, IDomainNotificationMediatorService domainNotification)
        {
            _uow = uow;
            _domainNotification = domainNotification;
        }
        public async Task<GenericCommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _uow.ProductRepository.GetById(request.Id);

            if (product is null)
            {
                _domainNotification.Notify(new Commons.DomainNotification("Error", "This product does not exist!"));
                return new GenericCommandResult(false, "Invalid Request!");
            }

            product.ChangeName(request.Name);
            product.ChangeBrand(request.Brand);
            product.ChangePrice(request.Price);
            product.UpdatedAt = DateTime.Now;

            try
            {
                await _uow.ProductRepository.Update(product);
                await _uow.Commit();
                return new GenericCommandResult(true, "Product successfully updated!");
            }
            catch (Exception ex)
            {
                throw new DomainUnprocessableEntityException(ex.Message);
            }
        }
    }
}