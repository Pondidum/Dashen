using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Dashen.Infrastructure;
using StructureMap;
using StructureMap.Graph;

namespace Dashen
{
	public class Dashboard
	{
		private readonly HttpSelfHostServer _server;
		private readonly DefinitionCollection _definitions;

		public Dashboard(Uri listenOn)
		{
			var config = new HttpSelfHostConfiguration(listenOn);
			
			var container = new Container(c => c.Scan(a =>
			{
				a.TheCallingAssembly();
				a.WithDefaultConventions();
				a.LookForRegistries();
			}));

			config.DependencyResolver = new StructureMapDependencyResolver(container);

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
			_definitions = container.GetInstance<DefinitionCollection>();
		}

		public void Start()
		{
			_server.OpenAsync().Wait();
		}

		public void Stop()
		{
			_server.CloseAsync().Wait();
		}

		public void RegisterModel(Definition definition)
		{
			_definitions.Add(definition);
		}
	}
}
