
using System;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Requests.Product
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public Decimal Price { get; set; }
    }
}