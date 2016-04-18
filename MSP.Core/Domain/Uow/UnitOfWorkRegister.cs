using Castle.Core;
using Castle.MicroKernel;
using MSP.Core.Dependency;
using System.Linq;
using System.Reflection;

namespace MSP.Core.Domain.Uow
{
    public class UnitOfWorkRegister
    {
        public static void Register(IIocManager IocManager)
        {
            IocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler)
        {
            if (UnitOfWorkHelper.IsConventionalUowClass(handler.ComponentModel.Implementation))
            {
               handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
            else if (handler.ComponentModel.Implementation.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(UnitOfWorkHelper.HasUnitOfWorkAttribute))
            {
               handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
    }
}
