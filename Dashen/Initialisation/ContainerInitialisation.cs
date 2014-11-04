using System.Web.Http;
using Dashen.Infrastructure;
using StructureMap;

namespace Dashen.Initialisation
{
	public class ContainerInitialisation : IDashboardInitialisation
	{
		private readonly IContainer _container;

		public ContainerInitialisation(IContainer container)
		{
			_container = container;
		}

		public void ApplyTo(HttpConfiguration config)
		{
			var original = config.DependencyResolver;
			var custom = new StructureMapDependencyResolver(_container);

			config.DependencyResolver = new CompositeDependencyResolver(original, custom);
		}
	}
}
