using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FiiPrezent.Interfaces
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);

        Task<ICollection<TEntity>> ListAllAsync();

        Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        
        Task<TEntity> AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}