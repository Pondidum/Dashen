using System.IO;
using System.Reflection;

namespace Dashen.Content
{
	public class StaticContentCache
	{
		private readonly Assembly _assembly;

		public StaticContentCache()
		{
			_assembly = typeof(Dashboard).Assembly;
		}

		public Stream GetContent(string directory, string file)
		{
			var path = directory + "." + file;
			var asmStream = _assembly.GetManifestResourceStream(GetType(), path);

			return asmStream ?? Stream.Null;
		}
	}
}
