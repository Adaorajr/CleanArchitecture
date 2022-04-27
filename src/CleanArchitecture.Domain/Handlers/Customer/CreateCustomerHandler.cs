using CleanArchitecture.Domain.Commands.Requests.Customer;
using CleanArchitecture.Domain.Commands.Responses.Customer;
using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Events.Customer;
using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Customer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, GenericCommandResult>
    {
        private readonly IUnitOfWorkContextDois _uow;
        private readonly IMediator _mediator;
        public CreateCustomerHandler(IUnitOfWorkContextDois uow, IMediator mediator)
        {
            _uow = uow;
            _mediator = mediator;
        }
        public async Task<GenericCommandResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _uow.CustomerRepository.Create(new Entities.Customer(request.Name));
            await _uow.Commit();

            CreateCustomerResponse response = customer;

            await _mediator.Publish(new CreatedCustomerEvent { Customer = customer.Name }, cancellationToken);

            return new GenericCommandResult<CreateCustomerResponse>(true, "Customer created successfully!", response);
        }
    }
}
