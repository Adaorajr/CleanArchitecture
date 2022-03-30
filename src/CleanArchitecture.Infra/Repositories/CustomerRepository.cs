using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Repositories
{
    public class CustomerRepository : EntityRepository<Customer, AppDataContextDois>, ICustomerRepository
    {
        public CustomerRepository(AppDataContextDois context): base(context)
        {

        }
    }
}
