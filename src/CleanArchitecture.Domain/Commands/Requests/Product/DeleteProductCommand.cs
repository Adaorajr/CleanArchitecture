using System;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Requests.Product
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}