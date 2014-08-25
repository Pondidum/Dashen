using System.Web.Http.SelfHost;

namespace Dashen.Configuration
{
	public interface IDashboardConfiguration
	{
		void ApplyTo(HttpSelfHostConfiguration config);
	}
}
