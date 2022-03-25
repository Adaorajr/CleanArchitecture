using System;
using CleanArchitecture.Domain.Queries.Responses.Product;
using MediatR;

namespace CleanArchitecture.Domain.Queries.Requests.Product
{
    public class GetProductByIdQuery : IRequest<GetProductByIdResponse>
    {
        public Guid Id { get; set; }
    }
}