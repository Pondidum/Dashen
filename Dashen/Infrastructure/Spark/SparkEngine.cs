using Spark;

namespace Dashen.Infrastructure.Spark
{
	public class SparkEngine
	{
		private readonly DashenConfiguration _configuration;
		private readonly ISparkViewEngine _engine;
		private readonly DescriptorBuilder _descriptorBuilder;
		private readonly ApplicationModel _applicationModel;

		public SparkEngine(DashenConfiguration configuration, ISparkViewEngine configuredEngine, DescriptorBuilder descriptorBuilder, ApplicationModel applicationModel)
		{
			_configuration = configuration;
			_engine = configuredEngine;
			_descriptorBuilder = descriptorBuilder;
			_applicationModel = applicationModel;
		}

		public DashenView CreateView(object model)
		{
			var descriptor = _descriptorBuilder.Build(model.GetType());
			var entry = _engine.CreateEntry(descriptor);

			var view = (DashenView)entry.CreateInstance();
			view.App = _applicationModel;
			view.Configuration = _configuration;

			((IDashenView)view).SetModel(model);

			return view;
		}
	}
}
