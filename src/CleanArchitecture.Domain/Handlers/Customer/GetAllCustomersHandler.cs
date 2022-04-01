using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using CleanArchitecture.Domain.Queries.Requests.Customer;
using CleanArchitecture.Domain.Queries.Responses.Customer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Handlers.Customer
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, List<GetCustomersResponse>>
    {
        private readonly IUnitOfWorkContextDois _uow;
        public GetAllCustomersHandler(IUnitOfWorkContextDois uow)
        {
            _uow = uow;
        }
        public async Task<List<GetCustomersResponse>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _uow.CustomerRepository.GetAll();
            var result = customers.Select(m => new GetCustomersResponse
            {
                Id = m.Id,
                Name = m.Name,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            }).ToList();
            return result;
        }
    }
}
