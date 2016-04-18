using MSP.Core.Domain.Uow;
using System.Reflection;

namespace MSP.Core
{
    internal class MSPKernelModule:MSPModule
    {
        public override void PerInitialize()
        {
            UnitOfWorkRegister.Register(Engine.iocManager);
        }

        public override void Initialize()
        {
            Engine.iocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
