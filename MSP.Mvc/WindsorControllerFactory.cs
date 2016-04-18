using MSP.Core.Dependency;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MSP.Mvc
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IIocManager _iocManager;
        public WindsorControllerFactory(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }

        public override void ReleaseController(IController controller)
        {
            _iocManager.Release(controller);
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
            return _iocManager.Resolve<IController>(controllerType);
        }
    }
}