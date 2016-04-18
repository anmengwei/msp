using MSP.Core;
using MSP.Core.Dependency;
using MSP.Core.Reflection;
using System;
using System.Reflection;
using System.Web.Mvc;

namespace MSP.Mvc
{
    public abstract class MSPApplication: System.Web.HttpApplication
    {
        private Assembly assembly { get; set; }
        public MSPApplication(Assembly assembly)
        {
            this.assembly = assembly;
        }
        protected virtual void Application_Start(object sender, EventArgs e)
        {
            //mvc 预处理
            Engine.iocManager.Register<IAssemblyFinder, WebAssemblyFinder>();
            Engine.iocManager.RegisterAssemblyByBaseOn(assembly, typeof(IController), DependencyLifeStyle.Transient);
            Engine.Start();
        }
        protected virtual void Application_End(object sender, EventArgs e)
        {
            Engine.End();
        }
    }
}
