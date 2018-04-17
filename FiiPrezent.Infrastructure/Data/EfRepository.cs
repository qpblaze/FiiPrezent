using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FiiPrezent.Infrastructure.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual TEntity GetById(Guid id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<ICollection<TEntity>> ListAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}