using System;
using CleanArchitecture.Domain.Queries.Responses.Product;
using CleanArchitecture.Domain.Response;
using Flunt.Notifications;
using MediatR;

namespace CleanArchitecture.Domain.Queries.Requests.Product
{
    public class GetProductByIdQuery : Notifiable<Notification>, IRequest<GenericCommandResult>
    {
        public Guid Id { get; set; }
    }
}