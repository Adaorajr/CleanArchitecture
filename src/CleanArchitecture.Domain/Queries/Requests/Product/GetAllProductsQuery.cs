using System.Collections.Generic;
using CleanArchitecture.Domain.Queries.Responses.Product;
using MediatR;

namespace CleanArchitecture.Domain.Queries.Requests.Product
{
    public class GetAllProductsQuery : IRequest<List<GetProductByIdResponse>>
    {
    }
}