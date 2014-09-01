using System.Collections.Generic;
using System.IO;
using Spark;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class DescriptorBuilder
	{
		private readonly IViewFolder _viewFolder;
		private readonly List<SparkViewDescriptor.Accessor> _accessors;

		public DescriptorBuilder(IViewFolder viewFolder)
		{
			_viewFolder = viewFolder;
			_accessors = new List<SparkViewDescriptor.Accessor>();
			_accessors.Add(new SparkViewDescriptor.Accessor
			{
				Property = "string AsmVersion",
				GetValue = "System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()",
			});
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

			TemplatePaths(viewName)
				.Each(template => descriptor.AddTemplate(template));

			_accessors
				.Each(accessor => descriptor.AddAccessor(accessor.Property, accessor.GetValue));

			return descriptor;
		}
	}
}
