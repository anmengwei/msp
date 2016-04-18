using MSP.Core.Dependency;
using MSP.Core.Domain.Uow;
using NHibernate;
using System;

namespace MSP.NHibernate.Uow
{
    public class NhUnitOfWork : IUnitOfWork, ITransientDependency
    {
        public ISession Session { get; set; }
        private ITransaction transaction { get; set; }
        private ISessionFactory sessionFactory { get; set; }
        private bool successed { get; set; }
        public string Id { get; set; }
        public bool IsDisposed { get; set; }

        public event EventHandler Completed;
        public event EventHandler Faild;

        public NhUnitOfWork(ISessionFactory sessionFactory)
        {
            this.Id = Guid.NewGuid().ToString("N");
            this.sessionFactory = sessionFactory;
        }
        public void Begin()
        {
            Session = sessionFactory.OpenSession();
            transaction = Session.BeginTransaction();
            transaction.Begin();
        }

        public void Complete()
        {
            Session.Flush();
            transaction.Commit();
            successed = true;
            Completed(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            if (!successed)
            {
                Faild(this, EventArgs.Empty);
            }
            IsDisposed = true;
            if (transaction != null)
            {
                transaction.Dispose();
            }
            if (Session != null)
            {
                Session.Dispose();
            }
        }
    }
}
