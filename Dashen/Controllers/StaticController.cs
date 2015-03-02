using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Dashen.Infrastructure;
using Dashen.Static;

namespace Dashen.Controllers
{
	public class StaticController : ApiController
	{
		private readonly StaticContentProvider _cache;

		public StaticController(StaticContentProvider cache)
		{
			_cache = cache;
		}

		public HttpResponseMessage Get(string directory, string file)
		{
			var resource = _cache.GetContent(directory, file);

			return new HttpResponseMessage
			{
				Content = new StreamContent(new MemoryStream(resource.Content))
				{
					Headers = { ContentType = new MediaTypeHeaderValue(resource.MimeType) }
				}
			};
		}
	}
}
