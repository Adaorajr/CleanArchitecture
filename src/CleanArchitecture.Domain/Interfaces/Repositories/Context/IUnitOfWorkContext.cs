using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Interfaces.Repositories.Context
{
    public interface IUnitOfWorkContext
    {
        IProductRepository ProductRepository { get; }
        Task<int> Commit();
        void Dispose();
    }
}
