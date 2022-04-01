using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using CleanArchitecture.Infra.Context;

namespace CleanArchitecture.Infra.Repositories.ContextDois
{
    public class CustomerRepository : EntityRepository<Customer, AppDataContextDois>, ICustomerRepository
    {
        public CustomerRepository(AppDataContextDois context): base(context)
        {

        }
    }
}
