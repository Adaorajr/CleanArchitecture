using CleanArchitecture.Domain.Commands.Requests.Product;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Domain.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Product
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, GenericCommandResult>
    {
        private readonly IUnitOfWorkContext _uow;
        public UpdateProductHandler(IUnitOfWorkContext uow)
        {
            _uow = uow;
        }
        public async Task<GenericCommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new GenericCommandResult(false, "Please, check:", request.Notifications);

            var product = await _uow.ProductRepository.GetById(request.Id);

            if (product is null)
            {
                request.AddNotification("Error", "This product does not exist!");
                return new GenericCommandResult(false, "Please, check:", request.Notifications);
            }

            product.ChangeName(request.Name);
            product.ChangeBrand(request.Brand);
            product.ChangePrice(request.Price);
            product.UpdatedAt = DateTime.Now;

            try
            {
                var result = await _uow.ProductRepository.Update(product);
                await _uow.Commit();
                return new GenericCommandResult(true, "Product successfully updated!", result);
            }
            catch (Exception ex)
            {
                throw new DomainUnprocessableEntityException(ex.Message);
            }
        }
    }
}