using System;
using System.Threading.Tasks;
using StructureMap;

namespace Dashen
{
	public class Dashboard
	{
		private readonly IContainer _container;
		private readonly DashboardConfiguration _config;
		private readonly ServerBuilder _serverBuilder;
		private readonly ModelInfoRepository _repository;
		private readonly IDGenerator _generator;

		public Dashboard(IContainer container, DashboardConfiguration config, ServerBuilder serverBuilder, ModelInfoRepository repository, IDGenerator generator)
		{
			_container = container;
			_config = config;
			_serverBuilder = serverBuilder;
			_repository = repository;
			_generator = generator;
		}

		public void Add<TModel>(Action<TModel> customise)
			where TModel : Model
		{
			var component = _container.GetInstance<Component<TModel>>();
			var model = _container.GetInstance<TModel>();
			var modelID = _generator.NextID();

			model.ID = modelID;

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
			var server = _serverBuilder.Create(_config.ListenOn);
			return server.OpenAsync();
		}
	}
}
