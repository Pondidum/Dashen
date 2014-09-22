using System;

namespace Dashen.Infrastructure.Spark
{
	public class NameTransformer
	{
		public string ViewFromModel(string modelName)
		{
			modelName = modelName
				.Replace("ViewModel", "")
				.Replace("Model", "");

			return String.Format("{0}.spark", modelName);
		}
	}
}
