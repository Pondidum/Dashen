using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure.Spark;
using Dashen.Models;

namespace Dashen.Controllers
{
	public class IndexController : ApiController
	{
		private readonly SparkResponseFactory _factory;

		public IndexController(SparkResponseFactory factory)
		{
			_factory = factory;
		}

		public HttpResponseMessage GetIndex()
		{
			return  _factory.From(new IndexViewModel { ApiUrl = "http://example.com" });
		}
	}
}
