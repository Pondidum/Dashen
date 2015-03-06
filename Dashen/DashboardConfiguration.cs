using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashen
{
	public class DashboardConfiguration
	{
		/// <summary>
		/// Url to listen on.  Defaults to http://localhost:8080
		/// </summary>
		public Uri ListenOn { get; set; }

		/// <summary>
		/// The name of the Application hosting the Dashboard
		/// </summary>
		public string ApplicationName { get; set; }

		/// <summary>
		/// The version of the Application hosting the Dashboard
		/// </summary>
		public string ApplicationVersion { get; set; }

		/// <summary>
		/// Custom resorces to use. Available under /static/user/{name}.
		/// </summary>
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
