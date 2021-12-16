using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IEntityRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Create(TEntity entity);
        Task<IEnumerable<TEntity>> CreateRange(IEnumerable<TEntity> entity);
        Task<TEntity> Update(TEntity entity);
        Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities);
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Delete(TEntity entity);
        Task DeleteAll(IEnumerable<TEntity> entities);
    }
}