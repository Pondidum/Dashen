using System.Web.Http.SelfHost;

namespace Dashen.Initialisation
{
	public interface IDashboardInitialisation
	{
		void ApplyTo(DashenConfiguration userConfig, HttpSelfHostConfiguration config);
	}
}
