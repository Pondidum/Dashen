using System.IO;
using System.Linq;
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
		private readonly DashboardConfiguration _configuration;

		public StaticController(StaticContentProvider cache, DashboardConfiguration configuration)
		{
			_cache = cache;
			_configuration = configuration;
		}

		public HttpResponseMessage Get(string directory, string file)
		{
			var resource = directory.EqualsIgnore("user")
				? GetUserResource(file)
				: _cache.GetContent(directory, file);

			return new HttpResponseMessage
			{
				Content = new StreamContent(new MemoryStream(resource.Content))
				{
					Headers = { ContentType = new MediaTypeHeaderValue(resource.MimeType) }
				}
			};
		}

		private Resource GetUserResource(string filename)
		{
			return _configuration.Resources.FirstOrDefault(r => r.Name.EqualsIgnore(filename)) ?? Resource.Empty;
		}
	}
}
