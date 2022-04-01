using CleanArchitecture.Domain.Interfaces.Repositories.Context;
using CleanArchitecture.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Repositories.Context
{
    public class UnitOfWorkContext : IUnitOfWorkContext, IDisposable
    {
        private readonly AppDataContext _context;
        public IProductRepository ProductRepository { get; private set; }

        public UnitOfWorkContext(AppDataContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(context);
        }

        public Task<int> Commit()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
