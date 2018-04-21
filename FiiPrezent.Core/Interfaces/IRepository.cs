using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FiiPrezent.Core.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task<ICollection<TEntity>> ListAllAsync(Expression<Func<TEntity, object>> include);

        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}