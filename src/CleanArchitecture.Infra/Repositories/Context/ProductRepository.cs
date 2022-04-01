using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Infra.Context;

namespace CleanArchitecture.Infra.Repositories.Context
{
    public class ProductRepository : EntityRepository<Product, AppDataContext>, IProductRepository
    {
        public ProductRepository(AppDataContext context) : base(context)
        {
        }
    }
}