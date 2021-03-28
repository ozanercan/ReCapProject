using Core.DataAccess.RepositoryPattern.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        }

        public DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

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

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).FirstOrDefault();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return Entities.ToList();
            else
                return Entities.Where(expression).ToList();
        }

        public List<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return Entities.AsNoTracking().ToList();
            else
                return Entities.Where(expression).AsNoTracking().ToList();
        }

        public TEntity GetNoTracking(Expression<Func<TEntity, bool>> expression)
        {
            return Entities.Where(expression).AsNoTracking().FirstOrDefault();
        }

        public async Task<bool> CreateBulkAsync(List<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
            return await CommitAsync();
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
            return await CommitAsync();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            Entities.Update(entity);
            return await CommitAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
            return await CommitAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return await Entities.ToListAsync();
            else
                return await Entities.Where(expression).ToListAsync();
        }


        public async Task<List<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return await Entities.AsNoTracking().ToListAsync();
            else
                return await Entities.Where(expression).AsNoTracking().ToListAsync();
        }


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Entities.Where(expression).FirstOrDefaultAsync();
        }


        public async Task<TEntity> GetNoTrackingAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Entities.Where(expression).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0 ? true : false;
        }
    }
}