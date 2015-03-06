using System;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
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
		private HttpSelfHostServer _server;

		public Dashboard(IContainer container, DashboardConfiguration config, ServerBuilder serverBuilder, ModelInfoRepository repository, IDGenerator generator)
		{
			_container = container;
			_config = config;
			_serverBuilder = serverBuilder;
			_repository = repository;
			_generator = generator;
		}

		/// <summary>Adds a widget to the dashboard.</summary>
		/// <typeparam name="TModel">The type of Widget to add</typeparam>
		/// <param name="customise">Populates the model.  Runs on each GET to /models/id/{id}</param>
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

		/// <summary>
		/// Starts the dashboard
		/// </summary>
		public Task Start()
		{
			_server = _server ?? _serverBuilder.Create(_config.ListenOn);

			return _server.OpenAsync();
		}

		/// <summary>
		/// Stops the dashboard
		/// </summary>
		public Task Stop()
		{
			return _server != null ? _server.CloseAsync() : Task.Delay(0);
		}
	}
}
