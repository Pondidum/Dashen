using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Dashen.Controllers
{
	public class ComponentsController : ApiController
	{
		private readonly ComponentRepository _components;

		public ComponentsController(ComponentRepository components)
		{
			_components = components;
		}

		public HttpResponseMessage Get(string name)
		{
			var contents = _components.GetComponent(name);

			return new HttpResponseMessage
			{
				Content = new StringContent(contents, Encoding.UTF8, "text/javascript")
			};
		}
	}
}
