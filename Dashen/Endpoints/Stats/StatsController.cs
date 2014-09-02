using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure.Spark;

namespace Dashen.Endpoints.Stats
{
	public class StatsController : ApiController
	{
		private readonly SparkResponseFactory _factory;
		private readonly DefinitionCollection _collection;
		private readonly DefinitionModelBuilder _builder;

		public StatsController(SparkResponseFactory factory, DefinitionCollection collection, DefinitionModelBuilder builder)
		{
			_factory = factory;
			_collection = collection;
			_builder = builder;
		}

		public HttpResponseMessage GetDispatch(string url = "")
		{
			var definition = _collection.GetByID(url);

			if (definition == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			var viewModel = _builder.BuildStatsViewModel(definition);

			return _factory.From(viewModel);
		}
	}
}
