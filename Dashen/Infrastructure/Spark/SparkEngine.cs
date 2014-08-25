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
		private readonly List<string> _noMaster;

		public SparkEngine()
		{
			var settings = new SparkSettings();
			settings.AddNamespace("System.Linq");
			settings.PageBaseType = typeof(DashenView).FullName;


			_engine = new SparkViewEngine(settings);
			_engine.ViewFolder = new EmbeddedViewFolder(GetType().Assembly, "Dashen.Views");

			_noMaster = new List<string>();
			_noMaster.Add("TextControl.spark");
			_noMaster.Add("ListControl.spark");
		}

		public DashenView CreateView(object model)
		{
			var modelName = model.GetType().Name;
			var viewName = String.Format("{0}.spark", modelName.Replace("ViewModel", ""));

			var descriptor = new SparkViewDescriptor();
			descriptor.AddTemplate(viewName);

			if (_noMaster.Contains(viewName) == false)
			{
				descriptor.AddTemplate(Path.Combine("Shared", "Application.spark"));
			}

			var entry = _engine.CreateEntry(descriptor);

			var view = (DashenView)entry.CreateInstance();
			((IDashenView)view).SetModel(model);

			return view;
		}
	}
}
