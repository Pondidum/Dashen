using System.Collections.Generic;
using System.IO;

namespace Dashen.Endpoints.Static
{
	public class CachingContentProvider : IStaticContentProvider
	{
		private readonly IStaticContentProvider _provider;
		private readonly Dictionary<string, ContentCache> _cache;
		private readonly object _locker;

		public CachingContentProvider(IStaticContentProvider provider)
		{
			_provider = provider;
			_cache = new Dictionary<string, ContentCache>();
			_locker = new object();
		}

		public StaticContent GetContent(string urlFragment)
		{
			if (_cache.ContainsKey(urlFragment) == false)
			{
				lock (_locker)
				{
					if (_cache.ContainsKey(urlFragment) == false)
					{
						var content = _provider.GetContent(urlFragment);

						_cache.Add(urlFragment, CacheEntryFromContent(content));
					}
				}
			}

			return StaticContentFromCacheEntry(_cache[urlFragment]);
		}

		private ContentCache CacheEntryFromContent(StaticContent content)
		{
			if (content == null) return null;

			using (var ms = new MemoryStream())
			{
				content.Stream.CopyTo(ms);

				return new ContentCache
				{
					StreamBytes = ms.ToArray(),
					MimeType = content.MimeType
				};
			}
		}

		private StaticContent StaticContentFromCacheEntry(ContentCache cache)
		{
			if (cache == null) return null;

			return new StaticContent
			{
				Stream = new MemoryStream(cache.StreamBytes),
				MimeType = cache.MimeType
			};
		}

		private class ContentCache
		{
			public byte[] StreamBytes { get; set; }
			public string MimeType { get; set; }
		}
	}
}
