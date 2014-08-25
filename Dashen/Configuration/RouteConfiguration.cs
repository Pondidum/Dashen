using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Dashen.Configuration
{
	public class RouteConfiguration : IDashboardConfiguration
	{
		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"Home",
				"",
				new { controller = "Index" }
			);

			config.Routes.MapHttpRoute(
				"Api",
				"stats/{*url}",
				new { controller = "Stats", action = "GetDispatch", url = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				"Static",
				"{*url}",
				new { controller = "Static", action = "GetDispatch" }
				);
		}
	}
}
