using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.DataAccess.RepositoryPattern.Abstract
{
    public interface IEfRepository<TEntity> where TEntity : class, IEntity, new()
    {
        bool Commit();

        bool CreateBulk(List<TEntity> entities);

        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Delete(TEntity entity);

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TEntity, object>>[] includes);

        List<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> expression = null, params Expression<Func<TEntity, object>>[] includes);

        TEntity Get(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

        TEntity GetNoTracking(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);

    }
}