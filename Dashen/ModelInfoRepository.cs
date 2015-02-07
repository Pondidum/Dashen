using System.Collections.Generic;
using System.Linq;
using Dashen.Infrastructure;

namespace Dashen
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

		public Model GetModel(string name)
		{
			return _info
				.Where(info => info.Component.Name.ToString().EqualsIgnore(name))
				.Select(info => info.Model.Invoke())
				.FirstOrDefault();
		}

		public IEnumerable<ModelInfo> All()
		{
			return _info;
		}
	}
}
