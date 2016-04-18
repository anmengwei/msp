using MSP.Core.Application;
using MSP.Core.Domain.Entities;
using MSP.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Spread.Application
{
    public interface IServiceBase<TEntity> : IApplicationService where TEntity : class, IEntity<int>
    {
        #region 常用操作 
        #region Select/Get/Query
        List<TEntity> GetAllList();
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(int id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        IPagedList<TEntity> PageList(Expression<Func<TEntity, bool>> predicate,int pageIndex, int pageSize);
        #endregion

        #region Insert
        int InsertAndGetId(TEntity entity);
        #endregion

        #region Update
        TEntity Update(TEntity entity);
        #endregion

        #region Delete
        void Delete(TEntity entity);
        void Delete(int id);
        void Delete(Expression<Func<TEntity, bool>> predicate);
        #endregion
        #endregion
    }
}
