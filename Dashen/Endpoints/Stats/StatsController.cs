using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure;
using Dashen.Infrastructure.Spark;
using Newtonsoft.Json.Linq;

namespace Dashen.Endpoints.Stats
{
	public class StatsController : ApiController
	{
		private readonly SparkResponseFactory _factory;
		private readonly WidgetCollection _widgets;
		private readonly DefinitionModelBuilder _builder;

		public StatsController(SparkResponseFactory factory, WidgetCollection collection, DefinitionModelBuilder builder)
		{
			_factory = factory;
			_widgets = collection;
			_builder = builder;
		}

		[HttpGet]
		public HttpResponseMessage Update(string url = "")
		{
			var definition = _widgets.GetByID(url);

			if (definition == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			return new HttpResponseMessage
			{
				Content = new JsonContent(JToken.FromObject(definition.Create()))
			};
		}

		[HttpGet]
		public HttpResponseMessage CreateWidget(string url = "")
		{
			var widget = _widgets.GetByID(url);

			if (widget == null)
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}

			var viewModel = _builder.BuildStatsViewModel(widget);

			return _factory.From(viewModel);
		}
	}
}
