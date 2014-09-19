using Dashen.Endpoints.Static;
using Dashen.Endpoints.Static.ContentProviders;
using Dashen.Infrastructure.Spark;
using Dashen.Initialisation;
using Spark;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Dashen
{
	public class DashenRegistry : Registry
	{
		public DashenRegistry()
		{
			For<ISparkViewEngine>().Use(c => c.GetInstance<SparkBuilder>().Build()).Singleton();

			For<WidgetCollection>().Singleton();

			Scan(a =>
			{
				a.TheCallingAssembly();
				a.AddAllTypesOf<IDashboardInitialisation>();
				a.AddAllTypesOf<IStaticContentProvider>();

				For<ReplacementSource>()
					.Singleton();

				For<AdhocContentProvider>()
					.Singleton();

				For<IStaticContentProvider>()
					.Use(c => new CompositeContentProvider(
						c.GetInstance<EmbeddedStaticContentProvider>(),
						c.GetInstance<AdhocContentProvider>()))
					.DecorateWith((c, i) => new ProcessedContentProvider(i, c.GetInstance<ReplacementSource>()))
					.DecorateWith(i => new CachingContentProvider(i))
					.Singleton();
			});
		}
	}
}
