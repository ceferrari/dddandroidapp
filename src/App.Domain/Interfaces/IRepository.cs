using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using App.Domain.Core.Models;

namespace App.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
        bool Merge(TEntity entity);
        bool InsertRange(ICollection<TEntity> entity);
        bool UpdateRange(ICollection<TEntity> entity);
        bool RemoveRange(ICollection<TEntity> entity);
        bool RemoveWhere(Expression<Func<TEntity, bool>> condicoes);

        TEntity FindByKey(params object[] key);
        TEntity Find(Expression<Func<TEntity, bool>> condicoes = null, bool noContexto = false);
        Count Count(Expression<Func<TEntity, bool>> condicoes = null);
        IQueryable<TEntity> List(Expression<Func<TEntity, bool>> condicoes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? offset = null, int? limit = null, bool noContexto = false);

        bool TruncteTable();
        bool SaveChanges();
    }
}
