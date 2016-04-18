using MSP.Core.Dependency;
using MSP.Core.Reflection;
using System;
using System.Collections.Generic;
namespace MSP.Core
{
    public static class Engine
    {

        public static IIocManager iocManager { get { return IocManager.Instance; }}
        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            iocManager.Register<ITypeFinder, TypeFinder>();
            var typeFinder = iocManager.IocContainer.Resolve<ITypeFinder>();
            var types = typeFinder.FindOfClassType<MSPModule>();


            List<MSPModule> InitModules = new List<MSPModule>();
            types.ForEach(x => { InitModules.Add((MSPModule)Activator.CreateInstance(x)); });

            InitModules.ForEach(x => x.PerInitialize());
            InitModules.ForEach(x => x.Initialize());
           
        }
        public static void End()
        {
            iocManager.Dispose();
        }
    }
}
