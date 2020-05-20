using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplement.Implements;

namespace TravelAgencyWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ITravelLogic TravelLogic { get; } = new TravelLogic();
        public static ITourLogic TourLogic { get; } = new TourLogic();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
    }
}
