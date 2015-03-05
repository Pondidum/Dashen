using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dashen.Infrastructure;

namespace Dashen.Static
{
	public class StaticContentProvider : IStaticContentProvider
	{
		private readonly Dictionary<string, Resource> _validPaths;

		public StaticContentProvider()
		{
			var type = GetType();

			var assembly = type.Assembly;
			var prefix = type.Namespace + Type.Delimiter;

			_validPaths = assembly
				.GetManifestResourceNames()
				.Select(name => new Resource(
					name.Replace(prefix, string.Empty),
					ContentTypeMap.GetMimeType(Path.GetExtension(name)),
					assembly.GetManifestResourceStream(name)))
				.ToDictionary(
					p => p.Name,
					p => p,
					StringComparer.OrdinalIgnoreCase);
		}

		public bool Handles(string directory)
		{
			return true;
		}

		public Resource GetResource(string directory, string filename)
		{
			var path = directory + Type.Delimiter + filename;
			Resource resource;

			return _validPaths.TryGetValue(path, out resource)
				? resource
				: Resource.Empty;

		}
	}
}
