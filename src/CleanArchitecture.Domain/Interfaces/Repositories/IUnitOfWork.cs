using System;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
        void Rollback();
    }
}