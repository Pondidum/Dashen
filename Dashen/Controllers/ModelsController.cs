using System.Net.Http;
using System.Web.Http;

namespace Dashen.Controllers
{
	public class ModelsController : ApiController
	{
		public HttpResponseMessage Get(string url = "")
		{
			return new HttpResponseMessage();
		}
	}
}
