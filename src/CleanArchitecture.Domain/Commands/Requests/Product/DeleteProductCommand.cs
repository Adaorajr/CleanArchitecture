using CleanArchitecture.Domain.Commons;
using MediatR;
using System;

namespace CleanArchitecture.Domain.Commands.Requests.Product
{
    public class DeleteProductCommand : Validatable, IRequest<GenericCommandResult>
    {
        public Guid Id { get; set; }

        public override void Validate()
        {
        }
    }
}