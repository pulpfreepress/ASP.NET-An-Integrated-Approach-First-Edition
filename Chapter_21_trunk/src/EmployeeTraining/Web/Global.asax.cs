using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {
          
        }

        protected void Session_Start(object sender, EventArgs e) {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {  }

        protected void Application_AuthenticateRequest(object sender, EventArgs e) {  }

        protected void Application_Error(object sender, EventArgs e) {  }

        protected void Session_End(object sender, EventArgs e) {  }

        protected void Application_End(object sender, EventArgs e) {  }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.Clear();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
          
            routes.MapRoute(
                "Help", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Help", action = "About", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "MasterViewPage", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "MasterViewPage", action = "ListEmployees", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
               "Admin", // Route name
               "{controller}/{action}/{id}", // URL with parameters
               new { controller = "Admin", action = "EmailTypeMaintenance", id = UrlParameter.Optional } // Parameter defaults
           );

            
        }

    }
}