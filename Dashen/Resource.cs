using System.IO;

namespace Dashen
{
	public class Resource
	{
		public static Resource Empty = new Resource("", "", new byte[0]);

		public string Name { get; set; }
		public string MimeType { get; set; }
		public byte[] Content { get; set; }

		public Resource(string name, string mimeType, Stream contents)
			: this(name, mimeType, AsBytes(contents))
		{
		}

		public Resource(string name, string mimeType, byte[] contents)
		{
			Name = name;
			MimeType = mimeType;
			Content = contents;
		}

		private static byte[] AsBytes(Stream stream)
		{
			var ms = new MemoryStream();
			stream.CopyTo(ms);

			return ms.ToArray();
		}
	}
}
