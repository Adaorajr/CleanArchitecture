using CleanArchitecture.Domain.Entities;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories.Context
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        DbConnection GetConnection();
    }
}