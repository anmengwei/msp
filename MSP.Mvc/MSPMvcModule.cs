using MSP.Core;
using System.Web.Mvc;

namespace MSP.Mvc
{
    public class MSPMvcModule:MSPModule
    {
        public override void Initialize()
        {

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Engine.iocManager));
        }
    }
}
