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
		private readonly ComponentRepository _components;
		private readonly ModelRepository _models;
		private readonly IDGenerator _generator;
		private readonly View _view;

		public Dashboard(IContainer container, ComponentRepository components, ModelRepository models, IDGenerator generator, View view)
		{
			_container = container;
			_components = components;
			_models = models;
			_generator = generator;
			_view = view;
		}

		public void Add<TComponent, TModel>(Action<TModel> customise)
			where TComponent : Component<TModel>
			where TModel : Model
		{

			var component = _container.GetInstance<TComponent>();
			var model = _container.GetInstance<TModel>();

			_components.Register(component);

			_models.Register(_generator.NextID(), () =>
			{
				customise(model);
				return model;
			});

			_view.AddAsset(new ComponentAssetInfo("components/" + component.Name));
		}

		public Task Start()
		{
			var config = new HttpSelfHostConfiguration("http://localhost:3030");
			config.DependencyResolver = new StructureMapDependencyResolver(_container);

			config.Routes.MapHttpRoute("Home", "", new { controller = "Index" });
			config.Routes.MapHttpRoute("Models", "models/{id}", new { controller = "Models" });
			config.Routes.MapHttpRoute("Components", "components/{name}", new { controller = "Components" });

			var host = new HttpSelfHostServer(config);

			return host.OpenAsync();
		}
	}
}
