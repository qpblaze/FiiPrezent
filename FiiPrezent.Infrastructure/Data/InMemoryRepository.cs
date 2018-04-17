using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FiiPrezent.Core.Entities;
using FiiPrezent.Core.Interfaces;

namespace FiiPrezent.Infrastructure.Data
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ICollection<TEntity> entities = new List<TEntity>();

        public Task<TEntity> GetByIdAsync(Guid id)
        {
            return Task.Run(() =>
                {
                    return entities.SingleOrDefault(x => x.Id == id);
                }
            );
        }

        public Task<ICollection<TEntity>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}