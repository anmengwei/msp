using Castle.Core;
using MSP.Core.Dependency;
using System;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;

namespace MSP.Core.Domain.Uow
{
    internal class CallContextCurrentUnitOfWorkProvider: ICurrentUnitOfWorkProvider,ITransientDependency
    {
        [DoNotWire]
        public IUnitOfWork Current { get { return GetCurrentUow(); } set { SetCurrentUow(value); } }

        private const string ContextKey = "MSP.UnitOfWork.Current";
        private static readonly ConcurrentDictionary<string, IUnitOfWork> UnitOfWorkDictionary = new ConcurrentDictionary<string, IUnitOfWork>();

        private static void SetCurrentUow(IUnitOfWork value)
        {
            if (value == null)
            {
                ExitFromCurrentUowScope();
                return;
            }
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey !=null)
            {
                IUnitOfWork unitOfWork;
                if (UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
                {
                    if (unitOfWork == value)
                    {
                        return;
                    }
                    value = unitOfWork;
                }
            }

            unitOfWorkKey = value.Id;
            if (!UnitOfWorkDictionary.TryAdd(unitOfWorkKey, value))
            {
                throw new Exception("Can not set unit of work! UnitOfWorkDictionary.TryAdd returns false!");
            }
            CallContext.LogicalSetData(ContextKey, unitOfWorkKey);

        }
        private static IUnitOfWork GetCurrentUow()
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                return null;
            }
            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }
            if (unitOfWork.IsDisposed)
            {
                UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }
            return unitOfWork;
        }

        private static void ExitFromCurrentUowScope()
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey == null)
            {
                return;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            UnitOfWorkDictionary.TryRemove(unitOfWorkKey,out unitOfWork);
            CallContext.FreeNamedDataSlot(ContextKey);
        }

    }
}
