using System;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace Dashen
{
	public class Dashboard
	{
		private readonly HttpSelfHostConfiguration _config;
		private readonly HttpSelfHostServer _server;

		public Dashboard(Uri listenOn)
		{
			_config = new HttpSelfHostConfiguration(listenOn);

			_config.Routes.MapHttpRoute("Default", "{controller}/{id}", new { id = RouteParameter.Optional });
			_server = new HttpSelfHostServer(_config);
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
