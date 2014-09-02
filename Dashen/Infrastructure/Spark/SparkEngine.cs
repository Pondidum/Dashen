using System;
using System.Collections.Generic;
using System.IO;
using Spark;
using Spark.FileSystem;

namespace Dashen.Infrastructure.Spark
{
	public class SparkEngine
	{
		private readonly SparkViewEngine _engine;
		private readonly DescriptorBuilder _descriptorBuilder;

		public SparkEngine()
		{
			var settings = new SparkSettings();
			settings.AddNamespace("System.Linq");
			settings.AddNamespace("Dashen.Infrastructure");
			settings.PageBaseType = typeof(DashenView).FullName;


			_engine = new SparkViewEngine(settings);
			_engine.ViewFolder = new EmbeddedViewFolder(GetType().Assembly, "Dashen");

			_descriptorBuilder = new DescriptorBuilder(_engine.ViewFolder, "Dashen");
		}

		public DashenView CreateView(object model)
		{
			var descriptor = _descriptorBuilder.Build(model.GetType());

			var entry = _engine.CreateEntry(descriptor);

			var view = (DashenView)entry.CreateInstance();
			((IDashenView)view).SetModel(model);

			return view;
		}
	}
}
