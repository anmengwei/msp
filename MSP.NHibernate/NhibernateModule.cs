using MSP.Core;
using MSP.Core.Dependency;
using MSP.Core.Domain.Repositories;
using MSP.NHibernate.Repositories;
using System.Reflection;

namespace MSP.NHibernate
{
    public class NhibernateModule : MSPModule
    {
        public override void Initialize()
        {
            IocManager.Instance.Register(typeof(IRepository<>), typeof(NhRepositoryBase<>), DependencyLifeStyle.Transient);
            IocManager.Instance.Register(typeof(IRepository<,>), typeof(NhRepositoryBase<,>), DependencyLifeStyle.Transient);
            IocManager.Instance.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
