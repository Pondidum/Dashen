using Dashen.Components;
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

					a.AddAllTypesOf(typeof(Component<>));
				});

				config.For<DashboardConfiguration>().Use(configuration);

				config.For<View>().Singleton();
				config.For<ModelInfoRepository>().Singleton();
			});

			var dash = container.GetInstance<Dashboard>();

			dash.Add<HeaderModel>(model =>
			{
				model.Title = "ERMAGHAD";
				model.AppName = configuration.ApplicationName;
				model.AppVersion = configuration.ApplicationVersion;
			});

			dash.Add<FooterModel>(model =>
			{
				model.DashenVersion = typeof(Dashboard).Assembly.GetName().Version.ToString();
			});

			return dash;
		}
	}
}
