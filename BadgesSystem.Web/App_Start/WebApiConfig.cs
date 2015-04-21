using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BadgesSystem.Web.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute(
                     name: "Users",
                     routeTemplate: "api/Users/{action}",
                     defaults: new { controller = "Users" });

            configuration.Routes.MapHttpRoute(
                     name: "View",
                     routeTemplate: "api/View/{action}",
                     defaults: new { controller = "View" });

            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            var cors = new EnableCorsAttribute("*", "*", "*");
            configuration.EnableCors(cors);
        }
    }
}