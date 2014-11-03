using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Dashen.Initialisation
{
	public class RouteInitialisation : IDashboardInitialisation
	{
		private readonly DashenConfiguration _userConfig;

		public RouteInitialisation(DashenConfiguration userConfig)
		{
			_userConfig = userConfig;
		}

		public void ApplyTo(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"Dashen.Home",
				_userConfig.ApplyPrefix(""),
				new { controller = "Index" }
			);

			config.Routes.MapHttpRoute(
				"Dashen.Widget",
				_userConfig.ApplyPrefix("Widgets/{action}/{*url}"),
				new { controller = "Widgets", url = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				"Dashen.Static",
				_userConfig.ApplyPrefix("{*url}"),
				new { controller = "Static", action = "GetDispatch" }
			);
		}
	}
}
