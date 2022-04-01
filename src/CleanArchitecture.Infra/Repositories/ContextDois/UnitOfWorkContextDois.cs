using CleanArchitecture.Domain.Interfaces.Repositories.ContextDois;
using CleanArchitecture.Infra.Context;
using CleanArchitecture.Infra.Repositories.ContextDois;
using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Repositories
{
    public class UnitOfWorkContextDois : IUnitOfWorkContextDois, IDisposable
    {
        private readonly AppDataContextDois _context;
        public ICustomerRepository CustomerRepository { get; private set; }

        public UnitOfWorkContextDois(AppDataContextDois context)
        {
            _context = context;
            CustomerRepository = new CustomerRepository(context);
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
