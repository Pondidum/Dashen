using StructureMap;

namespace Dashen
{
	public class DashboardBuilder
	{
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
				config.For<ModelInfoRepository>().Singleton();
			});

			return container.GetInstance<Dashboard>();
		}
	}
}
