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

		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"Home",
				_userConfig.ApplyPrefix(""),
				new { controller = "Index" }
			);

			config.Routes.MapHttpRoute(
				"Widget",
				_userConfig.ApplyPrefix("Widgets/{action}/{*url}"),
				new { controller = "Widgets", url = RouteParameter.Optional }
			);

			config.Routes.MapHttpRoute(
				"Static",
				_userConfig.ApplyPrefix("{*url}"),
				new { controller = "Static", action = "GetDispatch" }
			);
		}
	}
}
