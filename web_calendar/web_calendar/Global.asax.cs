using ApplicationRepository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using web_calendar.Handlers;
using web_calendar.Managers;

namespace web_calendar
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Initialise();

            string db_path = (Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))) + "\\ApplicationRepository").Remove(0, 6);
            AppDomain.CurrentDomain.SetData("DataDirectory", db_path);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Newtonsoft.Json.Formatting.Indented,
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

            NotificationManager notificationManager = new NotificationManager();
            notificationManager.SetManager();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var allowedOrigins = new[] { "http://localhost:54920" };
            var request = HttpContext.Current.Request;
            var response = HttpContext.Current.Response;
            var origin = request.Headers["Origin"];

            if (origin != null && allowedOrigins.Any(x => x == origin))
            {
                response.AddHeader("Access-Control-Allow-Origin", origin);
                response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                response.AddHeader("Access-Control-Allow-Headers", "Content-Type, X-Requested-With");
                response.AddHeader("Access-Control-Allow-Credentials", "true");
                if (request.HttpMethod == "OPTIONS")
                {
                    response.End();
                }
            }
        }
    }
}