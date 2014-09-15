using Spark;

namespace Dashen.Infrastructure.Spark
{
	public class SparkEngine
	{
		private readonly ISparkViewEngine _engine;
		private readonly DescriptorBuilder _descriptorBuilder;
		private readonly ApplicationModel _applicationModel;

		public SparkEngine(ISparkViewEngine configuredEngine, DescriptorBuilder descriptorBuilder, ApplicationModel applicationModel)
		{
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

			((IDashenView)view).SetModel(model);

			return view;
		}
	}
}
