using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure.Spark;

namespace Dashen.Endpoints.Index
{
	public class IndexController : ApiController
	{
		private readonly SparkResponseFactory _factory;
		private readonly WidgetCollection _definitions;
		private readonly DefinitionModelBuilder _builder;

		public IndexController(SparkResponseFactory factory, WidgetCollection definitions, DefinitionModelBuilder builder)
		{
			_factory = factory;
			_definitions = definitions;
			_builder = builder;
		}

		public HttpResponseMessage GetIndex()
		{
			return  _factory.From(new IndexViewModel
			{
				Definitions = _definitions.Select(d =>  _builder.BuildIndexDisplayViewModel(d))
			});
		}
	}
}
