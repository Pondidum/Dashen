using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace Dashen.Controllers
{
	public class ModelsController : ApiController
	{
		private readonly ModelRepository _models;

		public ModelsController(ModelRepository models)
		{
			_models = models;
		}

		public HttpResponseMessage Get(int id)
		{
			var model = _models.GetModel(id);

			return new HttpResponseMessage
			{
				Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "text/javascript")
			};
		}
	}
}
