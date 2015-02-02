using System.Net.Http;
using System.Text;
using System.Web.Http;
using Dashen.Properties;

namespace Dashen.Controllers
{
	public class ComponentsController : ApiController
	{
		private readonly ModelInfoRepository _components;

		public ComponentsController(ModelInfoRepository modelInfo)
		{
			_components = modelInfo;
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
