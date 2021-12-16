using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infra.Context;

namespace CleanArchitecture.Infra.Repositories
{
    public class ProductRepository : EntityRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDataContext context) : base(context)
        {
        }
    }
}