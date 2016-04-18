using Castle.DynamicProxy;

namespace MSP.Core.Domain.Uow
{
    public class UnitOfWorkInterceptor : IInterceptor
    {
        private readonly IUnitOfWorkManager unitOfWorkManager;

        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }
        public void Intercept(IInvocation invocation)
        {
            if (unitOfWorkManager.Current != null)
            {
                invocation.Proceed();
                return;
            }
            var unitOfWorkAttr = UnitOfWorkAttribute.GetUnitOfWorkAttributeOrNull(invocation.MethodInvocationTarget);
            if (unitOfWorkAttr == null || unitOfWorkAttr.IsDisabled)
            {
                invocation.Proceed();
                return;
            }
            using (var uow = unitOfWorkManager.Begin())
            {
                invocation.Proceed();
                uow.Complete();
            }
        }
    }
}
