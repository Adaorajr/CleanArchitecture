using System;
using CleanArchitecture.Domain.Response;
using Flunt.Notifications;
using MediatR;

namespace CleanArchitecture.Domain.Commands.Requests.Product
{
    public class DeleteProductCommand : Notifiable<Notification>, IRequest<GenericCommandResult>
    {
        public Guid Id { get; set; }
    }
}