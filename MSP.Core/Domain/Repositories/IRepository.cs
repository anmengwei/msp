using MSP.Core.Dependency;
using MSP.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MSP.Core.Domain.Repositories
{

    public interface IRepository: ITransientDependency
    {

    }
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        #region Select/Get/Query
        IQueryable<TEntity> GetAll();
        List<TEntity> GetAllList();
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        T Query<T>(Func<IQueryable<TEntity>, T> queryMethod);
        TEntity Get(TPrimaryKey id);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(TPrimaryKey id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Load(TPrimaryKey id);
        #endregion

        #region Insert
        TEntity Insert(TEntity entity);
        TPrimaryKey InsertAndGetId(TEntity entity);
        TEntity InsertOrUpdate(TEntity entity);
        TPrimaryKey InsertOrUpdateAndGetId(TEntity entity);
        #endregion

        #region Update
        TEntity Update(TEntity entity);
        TEntity Update(TPrimaryKey id, Action<TEntity> updateAction);
        #endregion

        #region Delete
        void Delete(TEntity entity);
        void Delete(TPrimaryKey id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Aggregates
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        long LongCount();
        long LongCount(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
    }
}
