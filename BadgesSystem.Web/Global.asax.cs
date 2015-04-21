using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using BadgesSystem.Web.App_Start;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System;

namespace BadgesSystem.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

    }
}
