using System.Web.Http.SelfHost;

namespace Dashen.Initialisation
{
	public class MessageHandlerInitialisation : IDashboardInitialisation
	{
		private readonly DashenConfiguration _userConfiguration;

		public MessageHandlerInitialisation(DashenConfiguration userConfiguration)
		{
			_userConfiguration = userConfiguration;
		}

		public void ApplyTo(HttpSelfHostConfiguration config)
		{
			foreach (var handler in _userConfiguration.MessageHandlers)
			{
				config.MessageHandlers.Add(handler);
			}
		}
	}
}
