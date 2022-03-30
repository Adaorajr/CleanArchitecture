using CleanArchitecture.Domain.Response;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Requests.Customer
{
    public class CreateCustomerCommand : Notifiable<Notification>, IRequest<GenericCommandResult>
    {
        public string Name { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
                .IsNotNullOrEmpty(Name, "Name", "The name can't be null or empty!")
                .IsTrue(Name.Length > 10, Name, "The name must be greather than 10 caracteres!")
                );              
        }
    }
}
