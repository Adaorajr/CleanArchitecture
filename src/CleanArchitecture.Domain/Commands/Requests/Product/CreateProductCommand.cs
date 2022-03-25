using System;
using CleanArchitecture.Domain.Response;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Requests.Product
{
    public class CreateProductCommand : Notifiable<Notification>, IRequest<GenericCommandResult>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public Decimal Price { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>().Requires()
            .IsNotNullOrEmpty(Name, "Name", "The name can't be null or empty!")
            .IsTrue(Name.Length > 5 && Name.Length < 100, "Name", "The name must be between 5 and 100 caracters!")
            .IsNotNullOrEmpty(Brand, "Brand", "The brand can't be null or empty")
            .IsTrue(Brand.Length > 2 && Brand.Length < 50, "Brand", "The brand must be between 2 and 50 caracters!")
            .IsNotNull(Price, "Price", "The price can't be null or empty!")
            );
        }
    }
}