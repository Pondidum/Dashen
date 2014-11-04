using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure.Spark;

namespace Dashen.Endpoints.Index
{
	public class IndexController : ApiController
	{
		private readonly SparkResponseFactory _factory;
		private readonly WidgetCollection _widgets;
		private readonly IndexViewModelBuilder _builder;

		public IndexController() : this(
			Dashboard.Container.GetInstance<SparkResponseFactory>(),
			Dashboard.Container.GetInstance<WidgetCollection>(),
			Dashboard.Container.GetInstance<IndexViewModelBuilder>())
		{
		}

		public IndexController(SparkResponseFactory factory, WidgetCollection widgets, IndexViewModelBuilder builder)
		{
			_factory = factory;
			_widgets = widgets;
			_builder = builder;
		}

		public HttpResponseMessage GetIndex()
		{
			return  _factory.From(new IndexViewModel
			{
				Definitions = _widgets.Select(_builder.FromWidget)
			});
		}
	}
}
