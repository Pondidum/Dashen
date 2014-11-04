using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Dashen.Initialisation;
using StructureMap;
using StructureMap.Graph;

namespace Dashen
{
	public class Dashboard
	{
		private readonly ServerBuilder _serverBuilder;
		private readonly WidgetCollection _definitions;
		private readonly Lazy<HttpSelfHostServer> _server;

		//i am so so sorry for this
		internal static IContainer Container { get; private set; }

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

			Container = container;

			_definitions = container.GetInstance<WidgetCollection>();
			_serverBuilder = container.GetInstance<ServerBuilder>();
			_server = new Lazy<HttpSelfHostServer>(() => _serverBuilder.BuildServer());
		}

		/// <summary>
		/// Starts the webui. Asynchronous
		/// </summary>
		public void Start()
		{
			_server.Value.OpenAsync().Wait();
		}

		/// <summary>
		/// Stops the webui, blocks until fully stopped.
		/// </summary>
		public void Stop()
		{
			if (_server.IsValueCreated)
			{
				_server.Value.CloseAsync().Wait();
			}
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

		public void HookTo(HttpConfiguration config)
		{
			_serverBuilder.ApplyTo(config);
		}
	}
}
