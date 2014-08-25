using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.SelfHost;

namespace Dashen.Configuration
{
	public class ServerBuilder
	{
		private readonly List<IDashboardConfiguration> _configurations;

		public ServerBuilder(IEnumerable<IDashboardConfiguration> configurations)
		{
			_configurations = configurations.ToList();
		}

		public HttpSelfHostServer BuildServer(Uri listenOn)
		{
			var config = new HttpSelfHostConfiguration(listenOn);

			_configurations.ForEach(c => c.ApplyTo(config));

			return new HttpSelfHostServer(config);
		}
	}
}
