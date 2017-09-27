using A.Core.PermissionModule.Services;
using A.Core.PermissionModule.WebAPI.Core;
using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace A.Core.PermissionModule.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Class2 s = null; //default implementation without xcopy
            //ADDTHIS
            GlobalConfiguration.Configure(CoreConfig.Register);
        }
    }
}
