using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure.Spark;
using Dashen.Models;

namespace Dashen.Controllers
{
	public class IndexController : ApiController
	{
		private readonly SparkResponseFactory _factory;
		private readonly DefinitionCollection _definitions;

		public IndexController(SparkResponseFactory factory, DefinitionCollection definitions)
		{
			_factory = factory;
			_definitions = definitions;
		}

		public HttpResponseMessage GetIndex()
		{
			return  _factory.From(new IndexViewModel
			{
				Definitions = _definitions.Select(d => new DefinitionModel
				{
					Heading = d.Heading,
					ID = d.ID,
					Url = "stats/" + d.ID,
					Interval = (int)d.Interval.TotalMilliseconds,
					Columns = 4,
				})
			});
		}
	}
}
