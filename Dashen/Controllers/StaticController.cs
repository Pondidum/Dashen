using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Dashen.Static;

namespace Dashen.Controllers
{
	public class StaticController : ApiController
	{
		private readonly StaticContentProvider _cache;
		private readonly UserContentProvider _userContent;

		public StaticController(StaticContentProvider cache, UserContentProvider userContent)
		{
			_cache = cache;
			_userContent = userContent;
		}

		public HttpResponseMessage Get(string directory, string file)
		{
			var resource = _userContent.Handles(directory)
				? _userContent.GetResource(directory, file)
				: _cache.GetResource(directory, file);

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
