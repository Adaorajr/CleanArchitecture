using CleanArchitecture.Domain.Commands.Requests.Customer;
using CleanArchitecture.Domain.Commands.Responses.Customer;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Response;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Customer
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, GenericCommandResult>
    {
        private readonly ICustomerRepository _customerRepository;
        public CreateCustomerHandler(ICustomerRepository customerRepository /*IUnitOfWork uow*/)
        {
            _customerRepository = customerRepository;
        }
        public async Task<GenericCommandResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            request.Validate();
            if (!request.IsValid)
                return new GenericCommandResult(false, "Please, check:", request.Notifications);

            var customer = await _customerRepository.Create(new Entities.Customer(request.Name));
            await _customerRepository.Commit();

            CreateCustomerResponse response = customer;
            return new GenericCommandResult(true, "Customer successfully created!", response);
        }
    }
}
