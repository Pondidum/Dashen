using System;
using System.Collections.Generic;

namespace Dashen
{
	public class ModelRepository
	{
		private readonly Dictionary<int, Func<Model>> _models;

		public ModelRepository()
		{
			_models = new Dictionary<int, Func<Model>>();
		}

		public void Register(int id, Func<Model> model)
		{
			_models.Add(id, model);
		}

		public Model GetModel(int id)
		{
			return _models[id]();
		}
	}
}
