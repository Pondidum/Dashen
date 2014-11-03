using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Dashen;

namespace WebapiTestApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

			var dash = new Dashboard(new DashenConfiguration());
			dash.HookTo(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

	        config.Routes.MapHttpRoute(
		        "Home",
		        "",
		        defaults: new { controller = "Home", action = "Get" }
			);

        }
    }
}
