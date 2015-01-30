﻿using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Dashen.Content;
using Dashen.Infrastructure;

namespace Dashen.Controllers
{
	public class StaticController : ApiController
	{
		private readonly StaticContentCache _cache;

		public StaticController(StaticContentCache cache)
		{
			_cache = cache;
		}

		public HttpResponseMessage Get(string directory, string file)
		{
			var content = _cache.GetContent(directory, file);
			var mime = ContentTypeMap.GetMimeType(Path.GetExtension(file));

			return new HttpResponseMessage
			{
				Content = new StreamContent(content)
				{
					Headers = { ContentType = new MediaTypeHeaderValue(mime) }
				}
			};
		}
	}
}
