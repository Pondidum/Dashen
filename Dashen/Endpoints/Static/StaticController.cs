using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Dashen.Endpoints.Static
{
	public class StaticController : ApiController
	{
		private readonly List<IStaticContentProvider> _contentProviders;

		public StaticController(IStaticContentProvider contentProvider)
		{
			_contentProviders = new[] {contentProvider}.ToList();
		}

		public HttpResponseMessage GetDispatch(string url = "")
		{
			var content = _contentProviders.Select(cp => cp.GetContent(url)).FirstOrDefault(c => c != null);

			if (content == null)
			{
				return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
			}

			var streamContent = new StreamContent(content.Stream);
			streamContent.Headers.ContentType = new MediaTypeHeaderValue(content.MimeType);

			return new HttpResponseMessage { Content = streamContent };
		}
	}

}
