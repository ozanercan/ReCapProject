using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.DataAccess.RepositoryPattern.Abstract
{
    public interface IEfRepository<TEntity> where TEntity : class, IEntity, new()
    {

        bool CreateBulk(List<TEntity> entities);
        Task<bool> CreateBulkAsync(List<TEntity> entities);

        bool Add(TEntity entity);
        Task<bool> AddAsync(TEntity entity);

        bool Update(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);

        bool Delete(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null);

        List<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> expression = null);
        Task<List<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> expression = null);

        TEntity Get(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        TEntity GetNoTracking(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetNoTrackingAsync(Expression<Func<TEntity, bool>> expression);

    }
}