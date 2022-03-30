using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Commands.Responses.Customer
{
    public class CreateCustomerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static implicit operator CreateCustomerResponse(Entities.Customer customer)
        {
            return new CreateCustomerResponse {
                Id = customer.Id,
                Name = customer.Name,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
