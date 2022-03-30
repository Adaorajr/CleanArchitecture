using CleanArchitecture.Domain.Queries.Responses.Customer;
using MediatR;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Queries.Requests.Customer
{
    public class GetAllCustomersQuery : IRequest<List<GetCustomersResponse>>
    {
    }
}
