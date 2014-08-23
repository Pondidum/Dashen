using System.Net.Http;
using System.Web.Http;

namespace Dashen.Controllers
{
	public class StatsController : ApiController
	{
		public HttpResponseMessage GetDispatch(string url = "")
		{
			return new HttpResponseMessage
			{
				Content = new StringContent("url is: " + url)
			};
		}
	}
}
