using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Dashen.Controllers
{
	public class StaticController : ApiController
	{
		private const string Prefix = "Dashen.Static.";
		private readonly Dictionary<string, Func<Stream>> _resources;

		public StaticController()
		{
			var assembly = GetType().Assembly;

			_resources = assembly
				.GetManifestResourceNames()
				.Where(name => name.StartsWith(Prefix))
				.ToDictionary(
					name => name.Substring(Prefix.Length),
					name => new Func<Stream>(() => assembly.GetManifestResourceStream(name)),
					StringComparer.OrdinalIgnoreCase);
		}

		public HttpResponseMessage GetDispatch(string url = "")
		{
			var path = url.Replace('/', '.');
			
			if (_resources.ContainsKey(path) == false)
			{
				return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
			}

			var stream = _resources[path].Invoke();

			return new HttpResponseMessage
			{
				Content = new StreamContent(stream)
			};
		}
	}
}
