using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Dashen.Infrastructure.Spark
{
	public class SparkResponseFactory
	{
		private readonly SparkEngine _engine;

		public SparkResponseFactory(SparkEngine engine)
		{
			_engine = engine;
		}

		public HttpResponseMessage From<TModel>(TModel model) where TModel : class
		{
			var view = _engine.CreateView(model);

			var content = new InternalPushStreamContent((responseStream, cont, context) =>
			{
				using (var writer = new StreamWriter(responseStream))
				{
					view.RenderView(writer);
				}
				responseStream.Close();
			});

			content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

			return new HttpResponseMessage(HttpStatusCode.OK)
			{
				Content = content
			};
		}

	}
}