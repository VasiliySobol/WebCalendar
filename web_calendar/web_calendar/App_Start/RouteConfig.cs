using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace web_calendar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Schedule",
                url: "Event/{action}/{id}",
                defaults: new { controller = "Event", action = "Schedule", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CreateByRef",
                url: "Event/{action}/{eventByRef}",
                defaults: new { controller = "Event", action = "CreateByRef", eventByRef = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Calendar", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
