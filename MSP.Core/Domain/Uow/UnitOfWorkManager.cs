using MSP.Core.Dependency;
using System;

namespace MSP.Core.Domain.Uow
{
    public class UnitOfWorkManager : IUnitOfWorkManager, ITransientDependency
    {
        private readonly ICurrentUnitOfWorkProvider currentUnitOfWorkProvider;
        public IUnitOfWork Current { get { return currentUnitOfWorkProvider.Current; }}

        public UnitOfWorkManager(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            this.currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }
        public IUnitOfWork Begin()
        {
            var uow = Engine.iocManager.Resolve<IUnitOfWork>();
            uow.Completed += (object sender, EventArgs e)=> 
            {
                //结束 释放当前线程工作单元
                currentUnitOfWorkProvider.Current = null;
                Engine.iocManager.Release(sender);
            };
            uow.Faild += (object sender, EventArgs e) =>
            {
                //失败 释放当前线程工作单元
                currentUnitOfWorkProvider.Current = null;
                Engine.iocManager.Release(sender);
            };
            uow.Begin();
            currentUnitOfWorkProvider.Current = uow;
            return uow;
        }
    }
}
