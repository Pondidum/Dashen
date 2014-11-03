using System;
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
		private readonly Dictionary<string, string> _contentRouteMap;

		public StaticController(IStaticContentProvider contentProvider)
		{
			_contentProviders = new[] {contentProvider}.ToList();
			_contentRouteMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			_contentRouteMap["css/normalize"] = "css/normalize.css";
			_contentRouteMap["css/foundation"] = "foundation.min.css";
			_contentRouteMap["css/style"] = "css/style.css";
			_contentRouteMap["js/jquery"] = "js/jquery-2.1.1.min.js";
			_contentRouteMap["js/jquery-flot"] = "js/jquery.flot.min.js";
		}

		public HttpResponseMessage GetDispatch(string url = "")
		{
			var path = _contentRouteMap[url];

			var content = _contentProviders.Select(cp => cp.GetContent(path)).FirstOrDefault(c => c != null);

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
