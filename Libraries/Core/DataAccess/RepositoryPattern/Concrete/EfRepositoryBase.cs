using Core.DataAccess.RepositoryPattern.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Core.DataAccess.RepositoryPattern.Concrete
{
    public class EfRepositoryBase<TEntity, TContext> : IEfRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private readonly DbContext _dbContext;

        public EfRepositoryBase()
        {
            _dbContext = new TContext();

            Query = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Query { get; set; }

        public DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        private void AddInclude(Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var include in includes)
                Query = Query.Include(include);
        }

        public bool Add(TEntity entity)
        {
            Entities.Add(entity);
            return Commit();
        }

        public bool Commit()
        {
            return _dbContext.SaveChanges() > 0 ? true : false;
        }

        public bool CreateBulk(List<TEntity> entities)
        {
            Entities.AddRange(entities);
            return Commit();
        }

        public bool Delete(TEntity entity)
        {
            Entities.Remove(entity);
            return Commit();
        }

        public bool Update(TEntity entity)
        {
            Entities.Update(entity);
            return Commit();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            AddInclude(includes);
            return Query.Where(expression).FirstOrDefault();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TEntity, object>>[] includes)
        {
            AddInclude(includes);
            if (expression == null)
                return Entities.ToList();
            else
                return Entities.Where(expression).ToList();
        }

        public List<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TEntity, object>>[] includes)
        {
            AddInclude(includes);
            if (expression == null)
                return Entities.AsNoTracking().ToList();
            else
                return Entities.Where(expression).AsNoTracking().ToList();
        }

        public TEntity GetNoTracking(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            AddInclude(includes);
            return Query.Where(expression).AsNoTracking().FirstOrDefault();
        }
    }
}