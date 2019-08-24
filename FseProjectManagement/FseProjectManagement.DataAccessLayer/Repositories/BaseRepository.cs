using FseProjectManagement.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.DataAccessLayer.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly FseProjectManagerDbContext context = new FseProjectManagerDbContext();

        public virtual TEntity Add(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);

            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange((IEnumerable<TEntity>)entities);

            return entities;
        }

        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate,
            int pageIndex = -1, int pageSize = 0)
        {
            var query = context.Set<TEntity>().Where(predicate);

            if (pageIndex > -1)
            {
                return query
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
            }

            return query;
        }

        public virtual TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public void Remove(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
