using System.Web.Http.SelfHost;
using Dashen.Infrastructure.Spark;

namespace Dashen.Initialisation
{
	public class SparkInitialisation : IDashboardInitialisation
	{
		private readonly SparkEngine _engine;

		public SparkInitialisation(SparkEngine engine)
		{
			_engine = engine;
		}

		public void ApplyTo(DashenConfiguration userConfig, HttpSelfHostConfiguration config)
		{
			var model = new ApplicationModel
			{
				Title = userConfig.Title,
				Version = userConfig.Version
			};

			_engine.SetApplicationModel(model);
		}
	}
}
