using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using App.Domain.Interfaces.Entities;

namespace App.Domain.Interfaces.Repositories
{
    public interface IRepository : IReadOnlyRepository
    {
        TEntity Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class, IEntity;

        TEntity Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class, IEntity;

        TEntity CreateOrUpdate<TEntity>(TEntity entity, string createdOrModifiedBy = null)
            where TEntity : class, IEntity;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity;

        void Delete<TEntity>(object id)
            where TEntity : class, IEntity;

        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity;

        void Save();

        Task SaveAsync();
    }
}