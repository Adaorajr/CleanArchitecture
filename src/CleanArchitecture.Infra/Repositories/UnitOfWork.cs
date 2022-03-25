using System.Threading.Tasks;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infra.Context;

namespace CleanArchitecture.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDataContext _context;
        public UnitOfWork(AppDataContext context)
        {
            _context = context;
        }
        public async Task<int> Commit() => await _context.SaveChangesAsync();

        public void Rollback()
        {
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}