using System;

namespace Dashen
{
	public class DashboardConfiguration
	{
		public Uri ListenOn { get; set; }

		public string ApplicationName { get; set; }
		public string ApplicationVersion { get; set; }

		internal string DashenVersion { get; set; }
	}
}
