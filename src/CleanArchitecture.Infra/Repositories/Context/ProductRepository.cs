using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Repositories.Context
{
    public class ProductRepository : EntityRepository<Product, AppDataContext>, IProductRepository
    {
        public ProductRepository(AppDataContext context) : base(context)
        {
        }

        public DbConnection GetConnection() => this._context.Database.GetDbConnection();
    }
}