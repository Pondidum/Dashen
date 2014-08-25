using System.Web.Http.SelfHost;
using Dashen.Infrastructure;

namespace Dashen.Configuration
{
	public class MessageHandlerConfiguration : IDashboardConfiguration
	{
		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			config.MessageHandlers.Add(new ConsoleLoggingHandler());
		}
	}
}
