using MSP.Core.Domain.Entities;
using MSP.Core.Domain.Repositories;
using MSP.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Spread.Application
{
    public class ServiceBase<TEntity>: IServiceBase<TEntity> where TEntity : class, IEntity<int>
    {
        private readonly IRepository<TEntity> EntityRep;

        public ServiceBase(IRepository<TEntity> BaseRep )
        {
            this.EntityRep = BaseRep;
        }

        #region 常用操作 

        #region Select/Get/Query
        public virtual List<TEntity> GetAllList()
        {
           return EntityRep.GetAllList();
        }
        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return EntityRep.GetAllList(predicate);
        }

        public virtual TEntity Get(int id)
        {
            return EntityRep.Get(id);
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return EntityRep.FirstOrDefault(predicate);
        }

        public virtual IPagedList<TEntity> PageList(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize)
        {
            if (predicate == null)
            {
                return new PagedList<TEntity>(EntityRep.GetAll(), pageIndex, pageSize);
            }
            else
            {
                return new PagedList<TEntity>(EntityRep.GetAll().Where(predicate), pageIndex, pageSize);
            }
          
        }

        #endregion

        #region Insert
        public virtual int InsertAndGetId(TEntity entity)
        {
            return EntityRep.InsertAndGetId(entity);
        }
        #endregion

        #region Update
        public virtual TEntity Update(TEntity entity)
        {
            return EntityRep.Update(entity);
        }
        #endregion

        #region Delete
        public virtual void Delete(TEntity entity)
        {
            EntityRep.Delete(entity);
        }
        public virtual void Delete(int id)
        {
            EntityRep.Delete(id);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            EntityRep.Delete(predicate);
        }
        #endregion

        #endregion
    }
}
