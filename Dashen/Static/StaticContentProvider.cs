using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
	
namespace Dashen.Static
{
	public class StaticContentProvider
	{
		private readonly Dictionary<string, Func<Stream>> _validPaths;

		public StaticContentProvider()
		{
			var type = GetType();

			var assembly = type.Assembly;
			var prefix = type.Namespace + Type.Delimiter;

			_validPaths = assembly
				.GetManifestResourceNames()
				.ToDictionary(
					p => p.Replace(prefix, string.Empty),
					p => new Func<Stream>(() => assembly.GetManifestResourceStream(p)),
					StringComparer.OrdinalIgnoreCase);
		}

		public Stream GetContent(string directory, string file)
		{
			var path = directory + Type.Delimiter + file;
			Func<Stream> func;

			return _validPaths.TryGetValue(path, out func) ? func() : Stream.Null;
		}
	}
}
