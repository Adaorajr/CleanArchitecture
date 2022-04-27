using CleanArchitecture.Domain.Commands.Requests.Customer;
using CleanArchitecture.Domain.Commands.Responses.Customer;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Customer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, GenericCommandResult>
    {
        private readonly IUnitOfWorkContextDois _uow;
        public CreateCustomerHandler(IUnitOfWorkContextDois uow)
        {
            _uow = uow;
        }
        public async Task<GenericCommandResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _uow.CustomerRepository.Create(new Entities.Customer(request.Name));
            await _uow.Commit();

            CreateCustomerResponse response = customer;

            return new GenericCommandResult<CreateCustomerResponse>(true, "Customer created successfully!", response);
        }
    }
}
