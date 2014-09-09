using System.Web.Http.SelfHost;
using Dashen.Infrastructure;

namespace Dashen.Initialisation
{
	public class MessageHandlerInitialisation : IDashboardInitialisation
	{
		public void ApplyTo(DashenConfiguration userConfig, HttpSelfHostConfiguration config)
		{
			userConfig.MessageHandlers.Each(config.MessageHandlers.Add);
		}
	}
}
