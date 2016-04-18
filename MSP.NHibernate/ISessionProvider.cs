using NHibernate;

namespace MSP.NHibernate
{
    public interface ISessionProvider
    {
        ISession Session { get; }
    }
}
