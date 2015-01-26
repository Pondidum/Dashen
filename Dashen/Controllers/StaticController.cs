using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Dashen.Content;

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

			return new HttpResponseMessage
			{
				Content = new StreamContent(content)
				{
					Headers = { ContentType = new MediaTypeHeaderValue("text/javascript") }
				}
			};
		}
	}
}
