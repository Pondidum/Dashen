using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.SelfHost;

namespace Dashen.Initialisation
{
	public class ServerBuilder
	{
		private readonly DashenConfiguration _config;
		private readonly List<IDashboardInitialisation> _initialisers;

		public ServerBuilder(DashenConfiguration config, IEnumerable<IDashboardInitialisation> initialisers)
		{
			_config = config;
			_initialisers = initialisers.ToList();
		}

		public HttpSelfHostServer BuildServer()
		{
			var serverConfig = new HttpSelfHostConfiguration(_config.ListenOn);

			_initialisers.ForEach(c => c.ApplyTo(serverConfig));

			return new HttpSelfHostServer(serverConfig);
		}
	}
}
