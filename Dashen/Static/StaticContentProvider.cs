using System.IO;
using System.Reflection;

namespace Dashen.Static
{
	public class StaticContentProvider
	{
		private readonly Assembly _assembly;

		public StaticContentProvider()
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
