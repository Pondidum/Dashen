using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure.Spark;
using Dashen.Models;

namespace Dashen.Controllers
{
	public class IndexController : ApiController
	{
		public HttpResponseMessage GetIndex()
		{
			var factory = new SparkResponseFactory(new SparkEngine());
			return  factory.From(new IndexViewModel { ApiUrl = "http://example.com" });
		}
	}
}
