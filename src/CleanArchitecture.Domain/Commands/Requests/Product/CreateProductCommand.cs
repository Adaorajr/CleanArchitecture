using CleanArchitecture.Domain.Commons;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using System;

namespace CleanArchitecture.Domain.Commands.Requests.Product
{
    public class CreateProductCommand : Validatable, IRequest<GenericCommandResult>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }

        public override void Validate()
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