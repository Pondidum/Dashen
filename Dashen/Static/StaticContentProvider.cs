using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dashen.Static
{
	public class StaticContentProvider
	{
		private readonly Assembly _assembly;
		private readonly string _prefix;
		private readonly Dictionary<string, string> _validPaths;

		public StaticContentProvider()
		{
			var type = GetType();

			_assembly = type.Assembly;
			_prefix = type.Namespace + Type.Delimiter;

			_validPaths = _assembly
				.GetManifestResourceNames()
				.ToDictionary(p => p, StringComparer.OrdinalIgnoreCase);
		}

		public Stream GetContent(string directory, string file)
		{
			var path = _prefix + directory + Type.Delimiter + file;

			if (_validPaths.ContainsKey(path) == false)
			{
				return Stream.Null;
			}

			var asmStream = _assembly.GetManifestResourceStream(_validPaths[path]);

			return asmStream ?? Stream.Null;
		}
	}
}
