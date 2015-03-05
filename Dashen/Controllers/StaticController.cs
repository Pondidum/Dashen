using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Dashen.Static;

namespace Dashen.Controllers
{
	public class StaticController : ApiController
	{
		private readonly List<IStaticContentProvider> _content;

		public StaticController(StaticContentProvider embeddedContent, UserContentProvider userContent)
		{
			_content = new List<IStaticContentProvider> { userContent, embeddedContent };
		}

		public HttpResponseMessage Get(string directory, string file)
		{
			var resource = _content
				.Where(provider => provider.Handles(directory))
				.Select(provider => provider.GetResource(directory, file))
				.FirstOrDefault() ?? Resource.Empty;

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
