using System;
using StructureMap;

namespace Dashen
{
	public class Dashboard
	{
		private readonly IContainer _container;
		private readonly ModelRepository _models;
		private readonly IDGenerator _generator;
		private readonly View _view;

		public Dashboard(IContainer container, ModelRepository models, IDGenerator generator, View view)
		{
			_container = container;
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

			_models.Register(_generator.NextID(), () =>
			{
				customise(model);
				return model;
			});

			_view.Register<TComponent, TModel>(component);

		}
	}
}
