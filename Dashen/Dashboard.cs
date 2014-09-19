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
		private readonly WidgetCollection _definitions;

		public Dashboard(DashenConfiguration config)
		{
			var container = new Container(c =>
			{
				c.For<DashenConfiguration>().Use(config);
				c.Scan(a =>
				{
					a.TheCallingAssembly();
					a.WithDefaultConventions();
					a.LookForRegistries();
				});
			});

			_definitions = container.GetInstance<WidgetCollection>();
			_server = container.GetInstance<ServerBuilder>().BuildServer();
		}

		/// <summary>
		/// Starts the webui. Asynchronous
		/// </summary>
		public void Start()
		{
			_server.OpenAsync().Wait();
		}

		/// <summary>
		/// Stops the webui, blocks until fully stopped.
		/// </summary>
		public void Stop()
		{
			_server.CloseAsync().Wait();
		}

		/// <summary>
		/// Registers a <see cref="Widget"/> to display in the webui.  
		/// Widgets are displayed in the order they are added.
		/// </summary>
		/// <param name="definition">The widget to add.</param>
		public void Register(Widget definition)
		{
			_definitions.Add(definition);
		}
	}
}
