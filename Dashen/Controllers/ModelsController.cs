using System.Net.Http;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;

namespace Dashen.Controllers
{
	public class ModelsController : ApiController
	{
		private readonly ModelInfoRepository _models;

		public ModelsController(ModelInfoRepository modelInfo)
		{
			_models = modelInfo;
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
