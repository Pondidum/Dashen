using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class DashenViewFolder : InMemoryViewFolder
	{
		public DashenViewFolder(Assembly assembly)
		{
			var ignoreCase = StringComparison.OrdinalIgnoreCase;

			var resources = assembly
				.GetManifestResourceNames()
				.Where(n => n.EndsWith(".spark", ignoreCase));

			foreach (var resourceName in resources)
			{
				using (var stream = assembly.GetManifestResourceStream(resourceName))
				{
					var contents = new byte[stream.Length];
					stream.Read(contents, 0, contents.Length);

					var extensionIndex = resourceName.LastIndexOf(".spark", ignoreCase);
					var pathEndIndex = resourceName.LastIndexOf(".", extensionIndex  - 1);

					var name = resourceName.Substring(pathEndIndex + 1);
					var directory = resourceName.Substring(0, pathEndIndex).Replace('.', Path.DirectorySeparatorChar);

					var path = Path.Combine(directory, name);
					Add(path, contents);
				}
			}
		}
	}
}
