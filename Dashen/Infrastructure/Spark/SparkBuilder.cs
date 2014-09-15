using Spark;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class SparkBuilder
	{
		private readonly DashenConfiguration _config;

		public SparkBuilder(DashenConfiguration config)
		{
			_config = config;
		}

		public ISparkViewEngine Build()
		{
			var settings = BuildSettings();
			var viewFolder = BuildViewFolder();

			var engine = new SparkViewEngine(settings)
			{
				ViewFolder = viewFolder
			};

			return engine;
		}

		private CombinedViewFolder BuildViewFolder()
		{
			var embedded = new EmbeddedViewFolder(GetType().Assembly, "Dashen");
			var user = new InMemoryViewFolder();
			
			return new CombinedViewFolder(embedded, user);
		}

		private static SparkSettings BuildSettings()
		{
			var settings = new SparkSettings();
			
			settings.AddNamespace("System.Linq");
			settings.AddNamespace("Dashen.Infrastructure");
			settings.PageBaseType = typeof(DashenView).FullName;

			return settings;
		}
	}
}
