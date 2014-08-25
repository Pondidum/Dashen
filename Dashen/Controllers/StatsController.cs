using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure.Spark;

namespace Dashen.Controllers
{
	public class StatsController : ApiController
	{
		private readonly SparkResponseFactory _factory;
		private readonly DefinitionCollection _collection;

		public StatsController(SparkResponseFactory factory, DefinitionCollection collection)
		{
			_factory = factory;
			_collection = collection;
		}

		public HttpResponseMessage GetDispatch(string url = "")
		{
			var definition = _collection.GetByName(url);

			if (definition == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			return _factory.From(definition.Create());
		}
	}
}
