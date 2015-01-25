using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Dashen.Controllers
{
	public class IndexController : ApiController
	{
		private readonly View _view;

		public IndexController(View view)
		{
			_view = view;
		}

		public HttpResponseMessage Get()
		{
			return new HttpResponseMessage
			{
				Content = new StringContent(_view.Render(), Encoding.UTF8, "text/html")
			};
		}
	}
}
