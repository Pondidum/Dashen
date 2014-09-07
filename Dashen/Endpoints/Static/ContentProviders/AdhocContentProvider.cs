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

		public void Add(string urlFragment, byte[] content, string mimeType)
		{
			Add(urlFragment, new ResourceContent { StreamBytes = content, MimeType = mimeType });
		}

		internal void Add(string urlFragment, ResourceContent content)
		{
			_resources.Add(urlFragment, content);
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

		internal class ResourceContent
		{
			public byte[] StreamBytes { get; set; }
			public string MimeType { get; set; }
		}
	}
}
