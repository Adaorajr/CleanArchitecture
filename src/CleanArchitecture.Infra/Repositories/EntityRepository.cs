using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CleanArchitecture.Infra.Repositories
{
    public class EntityRepository<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        public readonly TContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EntityRepository(TContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await Task.FromResult(_dbSet.Add(entity));
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> CreateRange(IEnumerable<TEntity> entity)
        {
            _dbSet.AddRange(entity);
            return await Task.FromResult(entity);
        }

        public virtual async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Task.CompletedTask;
        }

        public virtual async Task DeleteAll(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await Task.CompletedTask;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var result = await _dbSet.FirstOrDefaultAsync(t => t.Id == id);
            return result;
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            await Task.FromResult(_dbSet.Update(entity));
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return await Task.FromResult(entities);
        }
    }
}