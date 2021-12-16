using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Repositories;
using CleanArchitecture.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infra.Repositories
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public EntityRepository(AppDataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public virtual async Task<TEntity> Create(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> CreateRange(IEnumerable<TEntity> entity)
        {
            _dbSet.AddRange(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public virtual async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAll(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
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
            try
            {
                var x = _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var erro = ex.Message;

            }

            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();

            return entities;
        }
    }
}