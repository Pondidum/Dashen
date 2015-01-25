using System.Net.Http;
using System.Web.Http;

namespace Dashen.Controllers
{
	public class StaticController : ApiController
	{
		public HttpResponseMessage Get(string directory, string file)
		{
			return new HttpResponseMessage
			{
				Content = new StringContent(string.Format("Dir: {0}\nFile: {1}", directory, file))
			};
		}
	}
}
