using System;
using System.IO;
using Spark;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class SparkEngine 
	{
		private readonly SparkViewEngine _engine;

		public SparkEngine()
		{
			var settings = new SparkSettings();
			settings.AddNamespace("System.Linq");
			settings.PageBaseType = typeof(DashenView).FullName;
			
			_engine = new SparkViewEngine(settings);
			_engine.ViewFolder = new EmbeddedViewFolder(GetType().Assembly, "Dashen.Views");
		}

		public DashenView CreateView<TModel>(TModel model) where TModel : class
		{
			var modelName = model.GetType().Name;
			var viewName = String.Format("{0}.spark", modelName.Replace("ViewModel", ""));

			var descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate(viewName);
			descriptor.AddTemplate(Path.Combine("Shared", "Application.spark"));

			var entry = _engine.CreateEntry(descriptor);

			var view = (DashenView<TModel>)entry.CreateInstance();
			view.SetModel(model);

			return view;
		}
	}
}
