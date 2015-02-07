using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Dashen.Infrastructure;
using StructureMap;

namespace Dashen
{
	public class Dashboard
	{
		private readonly IContainer _container;
		private readonly DashboardConfiguration _config;
		private readonly ModelInfoRepository _repository;
		private readonly IDGenerator _generator;

		public Dashboard(IContainer container, DashboardConfiguration config, ModelInfoRepository repository, IDGenerator generator)
		{
			_container = container;
			_config = config;
			_repository = repository;
			_generator = generator;
		}

		public void Add<TComponent, TModel>(Action<TModel> customise)
			where TComponent : Component<TModel>
			where TModel : Model
		{

			var component = _container.GetInstance<TComponent>();
			var model = _container.GetInstance<TModel>();
			var modelID = _generator.NextID();

			var info = new ModelInfo
			{
				ModelID = modelID,
				Component = component,
				Model = () =>
				{
					customise(model);
					return model;
				}
			};

			_repository.Register(info);
		}

		public Task Start()
		{
			var config = new HttpSelfHostConfiguration(_config.ListenOn);
			config.DependencyResolver = new StructureMapDependencyResolver(_container);

			config.Routes.MapHttpRoute("Home", "", new { controller = "Index" });
			config.Routes.MapHttpRoute("Models.All", "models/all", new { controller = "Models", action = "getall" });
			config.Routes.MapHttpRoute("Models.Name", "models/name/{name}", new { controller = "Models", action = "getname" });
			config.Routes.MapHttpRoute("Models.ID", "models/{id}", new { controller = "Models" });
			config.Routes.MapHttpRoute("Components", "components/{name}", new { controller = "Components" });
			config.Routes.MapHttpRoute("Static", "static/{directory}/{file}", new { controller = "Static" });

			var host = new HttpSelfHostServer(config);

			return host.OpenAsync();
		}
	}
}
