using System.Collections.Generic;
using System.IO;
using Dashen.Infrastructure;

namespace Dashen.Endpoints.Static.ContentProviders
{
	public class AdhocContentProvider : IStaticContentProvider
	{
		private readonly Dictionary<string, ResourceContent> _resources;

		public AdhocContentProvider()
		{
			_resources = new Dictionary<string, ResourceContent>();
		}

		public void Add(string urlFragment, Stream content, string mimeType)
		{
			using (var ms = new MemoryStream())
			{
				content.CopyTo(ms);

				_resources.Add(urlFragment, new ResourceContent
				{
					StreamBytes = ms.ToArray(),
					MimeType = mimeType
				});
			}
		}

		public StaticContent GetContent(string urlFragment)
		{
			var resource  = _resources.Get(urlFragment);

			if (resource == null)
			{
				return null;
			}

			return new StaticContent
			{
				Stream = new MemoryStream(resource.StreamBytes), 
				MimeType = resource.MimeType
			};
		}

		private class ResourceContent
		{
			public byte[] StreamBytes { get; set; }
			public string MimeType { get; set; }
		}
	}
}
