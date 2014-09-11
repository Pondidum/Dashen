using System;
using System.Web.Http.SelfHost;
using Dashen.Initialisation;
using StructureMap;
using StructureMap.Graph;

namespace Dashen
{
	public class Dashboard
	{
		private readonly HttpSelfHostServer _server;
		private readonly DefinitionCollection _definitions;

		public Dashboard(DashenConfiguration config)
		{
			var container = new Container(c => c.Scan(a =>
			{
				a.TheCallingAssembly();
				a.WithDefaultConventions();
				a.LookForRegistries();
			}));

			_definitions = container.GetInstance<DefinitionCollection>();
			_server = container.GetInstance<ServerBuilder>().BuildServer(config);
		}

		public void Start()
		{
			_server.OpenAsync().Wait();
		}

		public void Stop()
		{
			_server.CloseAsync().Wait();
		}

		public void Register(Widget definition)
		{
			_definitions.Add(definition);
		}
	}
}
