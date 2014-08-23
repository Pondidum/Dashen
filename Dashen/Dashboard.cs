using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Dashen
{
	public class Dashboard
	{
		private readonly HttpSelfHostServer _server;

		public Dashboard(Uri listenOn)
		{
			var config = new HttpSelfHostConfiguration(listenOn);

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

			_server = new HttpSelfHostServer(config);
		}

		public void Start()
		{
			_server.OpenAsync().Wait();
		}

		public void Stop()
		{
			_server.CloseAsync().Wait();
		}
	}
}
