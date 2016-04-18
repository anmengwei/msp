using MSP.Core.Dependency;
using MSP.Core.Domain.Uow;
using MSP.NHibernate.Uow;
using NHibernate;

namespace MSP.NHibernate
{
    public class SessionProvider : ITransientDependency,ISessionProvider
    {
        private readonly ICurrentUnitOfWorkProvider currentUnitOfWorkProvider;
        public ISession Session
        {
            get
            {
                return currentUnitOfWorkProvider.Current.GetSession();
            }
        }

        public SessionProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            this.currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }
    }

    public static class UnitOfWorkProviderExtend
    {
        public static ISession GetSession(this IUnitOfWork unitOfWork)
        {
            return (unitOfWork as NhUnitOfWork).Session;
        }
    }
}
