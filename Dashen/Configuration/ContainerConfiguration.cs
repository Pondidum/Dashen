using System.Web.Http.SelfHost;
using Dashen.Infrastructure;
using StructureMap;

namespace Dashen.Configuration
{
	public class ContainerConfiguration : IDashboardConfiguration
	{
		private readonly IContainer _container;

		public ContainerConfiguration(IContainer container)
		{
			_container = container;
		}

		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			config.DependencyResolver = new StructureMapDependencyResolver(_container);
		}
	}
}
