using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Dashen.Initialisation
{
	public class RouteInitialisation : IDashboardInitialisation
	{
		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"Home",
				"",
				new { controller = "Index" }
			);

			config.Routes.MapHttpRoute(
				"Widget",
				"Widgets/{action}/{*url}",
				new { controller = "Widgets", url = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				"Static",
				"{*url}",
				new { controller = "Static", action = "GetDispatch" }
				);
		}
	}
}
