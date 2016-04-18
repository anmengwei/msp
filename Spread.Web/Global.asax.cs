using MSP.Mvc;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Spread.Web
{
    public class MvcApplication : MSPApplication
    {
        public MvcApplication() 
            : base(typeof(MvcApplication).Assembly)
        {
        }
        protected override void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            base.Application_Start(sender, e);
        }
        protected override void Application_End(object sender, EventArgs e)
        {
            base.Application_End(sender, e);
        }

    }
}
