using System.Web.Http.SelfHost;
using Dashen.Infrastructure;

namespace Dashen.Initialisation
{
	public class MessageHandlerInitialisation : IDashboardInitialisation
	{
		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			config.MessageHandlers.Add(new ConsoleLoggingHandler());
		}
	}
}
