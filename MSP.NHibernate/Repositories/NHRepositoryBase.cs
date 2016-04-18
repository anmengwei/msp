using NHibernate;
using NHibernate.Linq;
using System.Linq;
using MSP.Core.Domain.Repositories;
using MSP.Core.Domain.Entities;

namespace MSP.NHibernate.Repositories
{
    public class NhRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly ISessionProvider _sessionProvider;
        protected ISession Session { get { return _sessionProvider.Session; }}
        public NhRepositoryBase(ISessionProvider sessionProvider)
        {
            this._sessionProvider = sessionProvider;
            
        }
        public override IQueryable<TEntity> GetAll()
        {
           return Session.Query<TEntity>();
        }
        public override TEntity Load(TPrimaryKey id)
        {
            return Session.Load<TEntity>(id);
        }

        public override TEntity FirstOrDefault(TPrimaryKey id)
        {
            return Session.Get<TEntity>(id);
        }
        public override TEntity Insert(TEntity entity)
        {
            Session.Save(entity);
            return entity;
        }
        public override TEntity Update(TEntity entity)
        {
            Session.Update(entity);
            return entity;
        }
        public override TEntity InsertOrUpdate(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public override void Delete(TPrimaryKey id)
        {
            Session.Delete(Session.Load<TEntity>(id));
        }

        public override void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }
    }

    public class NhRepositoryBase<TEntity> : NhRepositoryBase<TEntity, int>,IRepository<TEntity> where TEntity : class, IEntity<int>
    {
        public NhRepositoryBase(ISessionProvider sessionProvider)
            :base(sessionProvider)
        {
        }
    }
}
