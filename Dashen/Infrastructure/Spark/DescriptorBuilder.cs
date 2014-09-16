using System;
using System.Collections.Generic;
using System.IO;
using Spark;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class DescriptorBuilder
	{
		private readonly IViewFolder _viewFolder;

		public DescriptorBuilder(ISparkViewEngine configuredEngine)
		{
			_viewFolder = configuredEngine.ViewFolder;
		}

		private bool HasTemplate(string path)
		{
			return _viewFolder.HasView(path);
		}

		internal IEnumerable<string> TemplatePaths(string viewPath, string viewName)
		{
			if (HasTemplate(Path.Combine("Dashen\\Views", viewName)))
			{
				yield return Path.Combine("Dashen\\Views", viewName);
			}

			if (HasTemplate(Path.Combine(viewPath, viewName)))
			{
				yield return Path.Combine(viewPath, viewName);

				if (HasTemplate(Path.Combine("Dashen\\Views", "Application.spark")))
				{
					yield return Path.Combine("Dashen\\Views", "Application.spark");
				}
			}
		}

		public SparkViewDescriptor Build(Type modelType)
		{
			var modelName = modelType.Name;
			var modelPath = modelType.FullName.Substring(0, modelType.FullName.Length - modelName.Length - 1);

			var viewName = String.Format("{0}.spark", modelName.Replace("ViewModel", ""));
			var viewPath = modelPath.Replace(".", "\\");

			var descriptor = new SparkViewDescriptor();

			TemplatePaths(viewPath, viewName)
				.Each(template => descriptor.AddTemplate(template));

			return descriptor;
		}
	}
}
