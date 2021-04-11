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
        public EfRepositoryBase()
        {
        }

        public bool Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry<TEntity>(entity).State = EntityState.Added;
                return context.SaveChanges() > 0;
            }
        }

        public bool CreateBulk(List<TEntity> entities)
        {
            using (var context = new TContext())
            {
                context.AddRange(entities);
                return context.SaveChanges() > 0;
            }
        }

        public bool Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry<TEntity>(entity).State = EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }

        public bool Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry<TEntity>(entity).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().Where(expression).FirstOrDefault();
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var context = new TContext())
            {
                if (expression == null)
                {
                    return context.Set<TEntity>().ToList();
                }
                else
                {
                    return context.Set<TEntity>().Where(expression).ToList();
                }
            }
        }

        public List<TEntity> GetAllNoTracking(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var context = new TContext())
            {
                if (expression == null)
                {
                    return context.Set<TEntity>().AsNoTracking().ToList();
                }
                else
                {
                    return context.Set<TEntity>().AsNoTracking().Where(expression).ToList();
                }
            }

        }

        public TEntity GetNoTracking(Expression<Func<TEntity, bool>> expression)
        {
            using (var context = new TContext())
            {
                if (expression == null)
                {
                    return context.Set<TEntity>().AsNoTracking().FirstOrDefault();
                }
                else
                {
                    return context.Set<TEntity>().AsNoTracking().Where(expression).FirstOrDefault();
                }
            }
        }

        public async Task<bool> CreateBulkAsync(List<TEntity> entities)
        {
            using (var context = new TContext())
            {
                await context.AddRangeAsync(entities);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry<TEntity>(entity).State = EntityState.Added;
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry<TEntity>(entity).State = EntityState.Modified;
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Entry<TEntity>(entity).State = EntityState.Deleted;
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            using (var context = new TContext())
            {
                if (expression == null)
                {
                    return await context.Set<TEntity>().ToListAsync();
                }
                else
                {
                    return await context.Set<TEntity>().Where(expression).ToListAsync();
                }
            }
        }


        public async Task<List<TEntity>> GetAllNoTrackingAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            //if (expression == null)
            //    return await Entities.AsNoTracking().ToListAsync();
            //else
            //    return await Entities.Where(expression).AsNoTracking().ToListAsync();

            using (var context = new TContext())
            {
                if (expression == null)
                {
                    return await context.Set<TEntity>().AsNoTracking().ToListAsync();
                }
                else
                {
                    return await context.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();
                }
            }
        }


        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().Where(expression).FirstOrDefaultAsync();
            }
        }


        public async Task<TEntity> GetNoTrackingAsync(Expression<Func<TEntity, bool>> expression)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().AsNoTracking().Where(expression).FirstOrDefaultAsync();
            }
        }
    }
}