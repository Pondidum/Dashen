using System.Net.Http;
using System.Web.Http;

namespace Dashen.Controllers
{
	public class IndexController : ApiController
	{
		public HttpResponseMessage GetIndex()
		{
			return new HttpResponseMessage { Content = new StringContent("Index win")};
		}
	}
}
