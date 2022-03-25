using System;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Requests.Product
{
    public class CreateProductCommand : IRequest<CreateProductResponse>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public Decimal Price { get; set; }
    }
}