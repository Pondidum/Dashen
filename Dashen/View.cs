using System;
using System.IO;
using System.Linq;
using System.Text;
using Dashen.Infrastructure;

namespace Dashen
{
	public class View
	{
		private readonly ModelInfoRepository _modelInfo;

		public View(ModelInfoRepository modelInfo)
		{
			_modelInfo = modelInfo;
		}

		private string GetTemplate()
		{
			using (var stream = GetType().Assembly.GetManifestResourceStream("Dashen.Static.views.Index.htm"))
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		private string GetComponents()
		{
			var components = _modelInfo
				.All()
				.Select(info => info.Component.Name)
				.Distinct()
				.Select(name => string.Format("<script type='text/jsx' src='components/{0}'></script>", name));

			return string.Join(Environment.NewLine, components);
		}

		public string Render()
		{
			return GetTemplate()
				.Replace("{components}", GetComponents());
		}

	}
}
