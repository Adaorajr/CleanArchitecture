using CleanArchitecture.Domain.DTO.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Queries
{
    public interface ICustomerQueries
    {
        Task<List<CustomerDTO>> GetAllCustomers();
    }
}
