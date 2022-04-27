using CleanArchitecture.Domain.Commons;
using CleanArchitecture.Domain.Interfaces.ExternalServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Events.Customer
{
    public class CreatedCustomerHandler : INotificationHandler<CreatedCustomerEvent>
    {
        private readonly IEmailService _email;

        public CreatedCustomerHandler(IEmailService emailService)
        {
            _email = emailService;
        }

        public async Task Handle(CreatedCustomerEvent notification, CancellationToken cancellationToken)
        {
            await _email.Send(
                    new Email(
                    "test tittle",
                    "test subject",
                    "test message",
                    new List<string>
                        { "teste@gmail.com" }
                    )
                );

            await Task.CompletedTask;
        }
    }
}
