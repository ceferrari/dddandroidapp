using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AndroidApp.Domain.Core.Models;
using AndroidApp.Domain.Interfaces.Repositories;
using AndroidApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AndroidApp.Infra.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly AndroidAppContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(AndroidAppContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual bool Insert(TEntity entity)
        {
            DbSet.Add(entity);
            return SaveChanges();
        }

        public virtual bool Update(TEntity entity)
        {
            DbSet.Update(entity);
            return SaveChanges();
        }

        public virtual bool Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            return SaveChanges();
        }

        public virtual bool Merge(TEntity entity)
        {
            //DbSet.AddOrUpdate(entity);
            return SaveChanges();
        }

        public virtual bool InsertRange(ICollection<TEntity> entity)
        {
            DbSet.AddRange(entity);
            return SaveChanges();
        }

        public virtual bool UpdateRange(ICollection<TEntity> entity)
        {
            DbSet.UpdateRange(entity);
            return SaveChanges();
        }

        public virtual bool RemoveRange(ICollection<TEntity> entity)
        {
            DbSet.RemoveRange(entity);
            return SaveChanges();
        }

        public virtual bool RemoveWhere(Expression<Func<TEntity, bool>> condicoes)
        {
            return RemoveRange(DbSet.Where(condicoes).ToList());
        }

        public virtual TEntity FindByKey(params object[] key)
        {
            return DbSet.Find(key);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> condicoes = null, bool noContexto = false)
        {
            var query = noContexto ? DbSet : DbSet.AsNoTracking();
            return condicoes == null ? query.FirstOrDefault() : query.FirstOrDefault(condicoes);
        }

        public virtual Count Count(Expression<Func<TEntity, bool>> condicoes = null)
        {
            return new Count(condicoes == null ? DbSet.AsNoTracking().Count() : DbSet.AsNoTracking().Count(condicoes));
        }

        public virtual IQueryable<TEntity> List(
            Expression<Func<TEntity, bool>> condicoes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? offset = null, int? limit = null, bool noContexto = false)
        {
            var query = noContexto ? DbSet : DbSet.AsNoTracking();
            if (condicoes != null)
            {
                query = query.Where(condicoes);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return offset != null && limit != null ? query.Skip(offset.Value).Take(limit.Value) : query;
        }

        public virtual bool TruncteTable()
        {
            return true;
            //return Db.Database.ExecuteSqlCommand($"TRUNCATE TABLE {Db.GetTableName<TEntity>()}") > 0;
        }

        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}