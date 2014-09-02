using System.IO;

namespace Dashen.Endpoints.Static
{
	public class StaticContent
	{
		public Stream Stream { get; set; }
		public string MimeType { get; set; }
	}
}
