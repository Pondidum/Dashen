using System.Collections.Generic;
using System.IO;
using Spark;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class DescriptorBuilder
	{
		private readonly IViewFolder _viewFolder;

		public DescriptorBuilder(IViewFolder viewFolder)
		{
			_viewFolder = viewFolder;
		}

		private bool HasTemplate(string path)
		{
			return _viewFolder.HasView(path);
		}

		internal IEnumerable<string> TemplatePaths(string viewName)
		{
			if (HasTemplate(Path.Combine("Shared", viewName)))
			{
				yield return Path.Combine("Shared", viewName);
			}

			if (HasTemplate(viewName))
			{
				yield return viewName;

				if (HasTemplate(Path.Combine("Shared", "Application.spark")))
				{
					yield return Path.Combine("Shared", "Application.spark");
				}
			}
		}

		public SparkViewDescriptor Build(string viewName)
		{
			var descriptor = new SparkViewDescriptor();

			TemplatePaths(viewName).Each(template => descriptor.AddTemplate(template));

			return descriptor;
		}
	}
}
