using System.Linq;
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

		public HttpResponseMessage GetName(string name)
		{
			var model = _models.GetModel(name);

			return new HttpResponseMessage
			{
				Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "text/javascript")
			};
		}

		public HttpResponseMessage GetType(string name)
		{
			var contents = _models.GetComponent(name);

			return new HttpResponseMessage
			{
				Content = new StringContent(contents, Encoding.UTF8, "text/javascript")
			};
		}

		public HttpResponseMessage GetAll()
		{
			var wrapperInfo = _models
				.All()
				.Where(model => model.Component.Unlisted == false)
				.Select(info => new
				{
					Type = info.Component.Name.ToString(),
					Path = "models/id/" + info.ModelID
				});

			var json = JsonConvert.SerializeObject(wrapperInfo);

			return new HttpResponseMessage
			{
				Content = new StringContent(json, Encoding.UTF8, "text/javascript")
			};
		}
	}
}
