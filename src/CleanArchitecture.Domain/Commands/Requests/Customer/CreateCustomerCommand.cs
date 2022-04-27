using CleanArchitecture.Domain.Commons;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Requests.Customer
{
    public class CreateCustomerCommand : Validatable, IRequest<GenericCommandResult>
    {
        public string Name { get; set; }

        public override void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsNotNullOrEmpty(Name, "Name", "The name can't be null or empty!")
                .IsGreaterThan(Name, 10, "Name", "The name must be greather than 10 caracteres!")
                );              
        }
    }
}
