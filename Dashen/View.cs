using System.IO;
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
			var sb = new StringBuilder();

			_modelInfo.All().ForEach(info =>
			{
				sb.AppendFormat("<script type='text/jsx' src='components/{0}'></script>", info.Component.Name);
				sb.AppendLine();
			});

			return sb.ToString();
		}

		public string Render()
		{
			return GetTemplate()
				.Replace("{components}", GetComponents());
		}

	}
}
