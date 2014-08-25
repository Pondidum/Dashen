using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dashen.Infrastructure
{
	public class ConsoleLoggingHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var sw = new Stopwatch();
			sw.Start();

			var response = await base.SendAsync(request, cancellationToken);

			sw.Stop();

			Console.WriteLine("{0} : {1}ms : {2}", request.Method, sw.ElapsedMilliseconds, request.RequestUri);
	
			return response;
		}
	}
}