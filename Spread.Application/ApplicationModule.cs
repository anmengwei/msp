using MSP.Core;
using MSP.Core.Dependency;
using System.Reflection;

namespace MSP.NHibernate
{
    public class ApplicationModule : MSPModule
    {
        public override void Initialize()
        {
            IocManager.Instance.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
