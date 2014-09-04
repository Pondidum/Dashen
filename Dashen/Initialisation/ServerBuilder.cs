using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.SelfHost;

namespace Dashen.Initialisation
{
	public class ServerBuilder
	{
		private readonly List<IDashboardInitialisation> _initialisers;

		public ServerBuilder(IEnumerable<IDashboardInitialisation> initialisers)
		{
			_initialisers = initialisers.ToList();
		}

		public HttpSelfHostServer BuildServer(DashenConfiguration config)
		{
			var serverConfig = new HttpSelfHostConfiguration(config.ListenOn);

			_initialisers.ForEach(c => c.ApplyTo(config, serverConfig));

			return new HttpSelfHostServer(serverConfig);
		}
	}
}
