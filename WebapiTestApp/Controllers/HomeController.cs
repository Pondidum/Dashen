using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace WebapiTestApp.Controllers
{
	public class HomeController : ApiController
	{
		[HttpGet]
		public HttpResponseMessage Get()
		{
			var content = new StringContent("<h1>Hi</h1>");
			content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

			return new HttpResponseMessage(HttpStatusCode.OK)
			{
				 Content = content
			};
		}
	}
}