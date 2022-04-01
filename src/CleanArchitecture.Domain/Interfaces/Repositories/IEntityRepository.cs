using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CleanArchitecture.Domain.Interfaces.Repositories
{
    public interface IEntityRepository<TEntity>
        where TEntity : BaseEntity
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