using System.IO;
using Spark;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class SparkBuilder
	{
		private readonly DashenConfiguration _config;
		private readonly NameTransformer _namer;

		public SparkBuilder(DashenConfiguration config, NameTransformer namer)
		{
			_config = config;
			_namer = namer;
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
			var embedded = new DashenViewFolder(GetType().Assembly);
			var user = new InMemoryViewFolder();

			foreach (var pair in _config.CustomViews)
			{
				var name = _namer.ViewFromModel(pair.Key.Name);
				var path = Path.Combine("Dashen\\Views\\", name);

				user.Add(path, pair.Value);
			}
			
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
