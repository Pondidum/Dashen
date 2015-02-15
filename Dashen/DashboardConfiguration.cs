using System;

namespace Dashen
{
	public class DashboardConfiguration
	{
		public Uri ListenOn { get; set; }

		public string ApplicationName { get; set; }
		public string ApplicationVersion { get; set; }

		public DashboardConfiguration()
		{
			ListenOn = new Uri("http://localhost:8080");
			ApplicationName = "Dashboard";
			ApplicationVersion = "1.0.0.0";
		}
	}
}
