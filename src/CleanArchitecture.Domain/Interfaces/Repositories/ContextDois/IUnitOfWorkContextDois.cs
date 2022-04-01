using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories.ContextDois
{
    public interface IUnitOfWorkContextDois
    {
        ICustomerRepository CustomerRepository { get; }
        Task<int> Commit();
        void Dispose();
    }
}
