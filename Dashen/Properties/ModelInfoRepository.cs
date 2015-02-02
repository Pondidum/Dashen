﻿using System.Collections.Generic;
using System.Linq;

namespace Dashen.Properties
{
	public class ModelInfoRepository
	{
		private readonly List<ModelInfo> _info;

		public ModelInfoRepository()
		{
			_info = new List<ModelInfo>();
		}

		public void Register(ModelInfo info)
		{
			_info.Add(info);
		}

		public string GetComponent(string componentName)
		{
			var name = ComponentName.From(componentName);

			return _info
				.Where(info => info.Component.Name.Equals(name))
				.Select(info => info.Component.GetJsx())
				.FirstOrDefault();
		}

		public Model GetModel(int id)
		{
			return _info
				.Where(info => info.ModelID == id)
				.Select(info => info.Model.Invoke())
				.FirstOrDefault();
		}
	}
}
