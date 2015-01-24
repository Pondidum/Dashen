using System;
using StructureMap;

namespace Dashen
{
	public class Dashboard
	{
		private readonly IContainer _container;

		public Dashboard(IContainer container)
		{
			_container = container;
		}

		public void Add<TComponent, TModel>(Action<TModel> customise)
			where TComponent : Component<TModel>
			where TModel : Model
		{

			var component = _container.GetInstance<TComponent>();
			var model = _container.GetInstance<TModel>();


		}
	}
}
