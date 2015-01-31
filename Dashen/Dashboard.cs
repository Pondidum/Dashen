using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Dashen.Assets;
using Dashen.Infrastructure;
using StructureMap;

namespace Dashen
{
	public class Dashboard
	{
		private readonly IContainer _container;
		private readonly DashboardConfiguration _config;
		private readonly ComponentRepository _components;
		private readonly ModelRepository _models;
		private readonly IDGenerator _generator;
		private readonly View _view;

		public Dashboard(IContainer container, DashboardConfiguration config, ComponentRepository components, ModelRepository models, IDGenerator generator, View view)
		{
			_container = container;
			_config = config;
			_components = components;
			_models = models;
			_generator = generator;
			_view = view;
		}

		public static Dashboard Create(DashboardConfiguration configuration)
		{
			var container = new Container(config =>
			{
				config.Scan(a =>
				{
					a.AssemblyContainingType<Dashboard>();
					a.WithDefaultConventions();
				});

				config.For<DashboardConfiguration>().Use(configuration);

				config.For<View>().Singleton();
				config.For<ModelRepository>().Singleton();
				config.For<ComponentRepository>().Singleton();
			});

			return container.GetInstance<Dashboard>();
		}

		public void Add<TComponent, TModel>(Action<TModel> customise)
			where TComponent : Component<TModel>
			where TModel : Model
		{

			var component = _container.GetInstance<TComponent>();
			var model = _container.GetInstance<TModel>();
			var modelID = _generator.NextID();

			_components.Register(component);

			_models.Register(modelID, () =>
			{
				customise(model);
				return model;
			});

			_view.AddAsset(new ComponentAssetInfo(component.Name, modelID));
		}

		public Task Start()
		{
			var config = new HttpSelfHostConfiguration(_config.ListenOn);
			config.DependencyResolver = new StructureMapDependencyResolver(_container);

			config.Routes.MapHttpRoute("Home", "", new { controller = "Index" });
			config.Routes.MapHttpRoute("Models", "models/{id}", new { controller = "Models" });
			config.Routes.MapHttpRoute("Components", "components/{name}", new { controller = "Components" });
			config.Routes.MapHttpRoute("Static", "static/{directory}/{file}", new { controller = "Static" });

			var host = new HttpSelfHostServer(config);

			return host.OpenAsync();
		}
	}
}
