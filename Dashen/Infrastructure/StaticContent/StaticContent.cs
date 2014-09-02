using System.IO;

namespace Dashen.Infrastructure.StaticContent
{
	public class StaticContent
	{
		public Stream Stream { get; set; }
		public string MimeType { get; set; }
	}
}
