using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using App.Domain.Interfaces.Entities;
using App.Domain.Interfaces.Repositories;

namespace App.Infra.Data.Repositories
{
    public class Repository<TContext> : ReadOnlyRepository<TContext>, IRepository
        where TContext : DbContext
    {
        public Repository(TContext context)
            : base(context)
        {
            Context.ChangeTracker.AutoDetectChangesEnabled = true;
            Context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }

        public virtual TEntity Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.CreatedBy = createdBy;
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual ICollection<TEntity> CreateRange<TEntity>(ICollection<TEntity> entities, string createdBy = null)
            where TEntity : class, IEntity
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.CreatedBy = createdBy;
            }
            Context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public virtual TEntity Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity
        {
            entity.ModifiedDate = DateTime.UtcNow;
            entity.ModifiedBy = modifiedBy;
            //Context.Set<TEntity>().Attach(entity);
            //Context.Entry(entity).State = EntityState.Modified;
            Context.Set<TEntity>().Update(entity);
            return entity;
        }

        public virtual TEntity CreateOrUpdate<TEntity>(TEntity entity, string createdOrModifiedBy = null)
            where TEntity : class, IEntity
        {
            return GetExists<TEntity>() ? Update(entity) : Create(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            Delete(entity);
        }

        public virtual void Delete<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            foreach (var entity in Get(filter))
            {
                Delete(entity);
            }
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual Task SaveAsync()
        {
            Context.SaveChangesAsync();
            return Task.FromResult(0);
        }
    }
}