using CleanArchitecture.Domain.Entities;
using System.Data.Common;

namespace CleanArchitecture.Domain.Interfaces.Repositories.Context
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        DbConnection GetConnection();
    }
}