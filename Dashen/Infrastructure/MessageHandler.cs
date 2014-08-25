using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dashen.Infrastructure
{
	public class ConsoleLoggingHandler : DelegatingHandler
	{
		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			//var corrId = string.Format("{0}:{1}", DateTime.Now.Ticks, Thread.CurrentThread.ManagedThreadId);
			//var requestInfo = string.Format("{0} {1}", request.Method, request.RequestUri);

			//var requestMessage = await request.Content.ReadAsByteArrayAsync();

			//await IncommingMessageAsync(corrId, requestInfo, requestMessage);
			var sw = new Stopwatch();
			sw.Start();

			var response = await base.SendAsync(request, cancellationToken);

			sw.Stop();

			Console.WriteLine("{0} : {1}ms : {2}", request.Method, sw.ElapsedMilliseconds, request.RequestUri);
			//byte[] responseMessage;

			//if (response.IsSuccessStatusCode)
			//	responseMessage = await response.Content.ReadAsByteArrayAsync();
			//else
			//	responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

			//await OutgoingMessageAsync(corrId, requestInfo, responseMessage);

			return response;
		}
	}
}