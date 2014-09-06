using Dashen.Endpoints.Static;
using Dashen.Endpoints.Static.ContentProviders;
using Dashen.Infrastructure.Spark;
using Dashen.Initialisation;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Dashen
{
	public class DashenRegistry : Registry
	{
		public DashenRegistry()
		{
			For<SparkEngine>().Singleton();
			For<DefinitionCollection>().Singleton();

			Scan(a =>
			{
				a.TheCallingAssembly();
				a.AddAllTypesOf<IDashboardInitialisation>();
				a.AddAllTypesOf<IStaticContentProvider>();

				For<IStaticContentProvider>()
					.Use<EmbeddedStaticContentProvider>()
					.DecorateWith((c, i) => new ProcessedContentProvider(i, c.GetInstance<ReplacementSource>()))
					.DecorateWith(i => new CachingContentProvider(i))
					.Singleton();

				For<ReplacementSource>()
					.Singleton();
			});
		}
	}
}
