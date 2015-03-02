using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashen
{
	public class DashboardConfiguration
	{
		public Uri ListenOn { get; set; }

		public string ApplicationName { get; set; }
		public string ApplicationVersion { get; set; }
		public IEnumerable<Resource> Resources { get; set; }

		public DashboardConfiguration()
		{
			ListenOn = new Uri("http://localhost:8080");
			ApplicationName = "Dashboard";
			ApplicationVersion = "1.0.0.0";
			Resources = Enumerable.Empty<Resource>();
		}
	}
}
