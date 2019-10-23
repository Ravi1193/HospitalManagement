using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HospitalManagementSystem.App_Start;
namespace HospitalManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
            BundleConfig.RegisterBundle(BundleTable.Bundles);
        }
    }
}
