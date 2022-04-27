using CleanArchitecture.Domain.DTO.Customer;
using CleanArchitecture.Domain.Interfaces.Queries;
using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Queries
{
    public class CustomerQueries : ICustomerQueries
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerQueries(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDTO>> GetAllCustomers()
        {
            var customer = await _customerRepository.GetAll();

            var result = customer.Select(m => new CustomerDTO
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
