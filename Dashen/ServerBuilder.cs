using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Dashen.Infrastructure;

namespace Dashen
{
	public class ServerBuilder
	{
		private readonly StructureMapDependencyResolver _resolver;

		public ServerBuilder(StructureMapDependencyResolver resolver)
		{
			_resolver = resolver;
		}

		public HttpSelfHostServer Create(Uri listenOn)
		{
			var config = new HttpSelfHostConfiguration(listenOn);
			config.DependencyResolver = _resolver;

			ConfigureRoutes(config);

			return new HttpSelfHostServer(config);
		}

		private static void ConfigureRoutes(HttpSelfHostConfiguration config)
		{
			config.Routes.MapHttpRoute(
				"Home",
				"",
				new {controller = "Index"});

			config.Routes.MapHttpRoute(
				"Models.All",
				"models/all",
				new {controller = "Models", action = "getall"});

			config.Routes.MapHttpRoute(
				"Models.Name",
				"models/name/{name}",
				new {controller = "Models", action = "getname"});

			config.Routes.MapHttpRoute(
				"Models.Type",
				"models/type/{name}",
				new {controller = "Models", action = "gettype"});

			config.Routes.MapHttpRoute(
				"Models.ID",
				"models/id/{id}",
				new {controller = "Models"});

			config.Routes.MapHttpRoute(
				"Static",
				"static/{directory}/{file}",
				new {controller = "Static"});
		}
	}
}
