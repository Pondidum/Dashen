using System.Collections.Generic;
using System.Text;

namespace Dashen
{
	public class View
	{
		private readonly List<AssetInfo> _assets;
		private readonly List<string> _jsx;

		public View()
		{
			_assets = new List<AssetInfo>();
			_jsx = new List<string>();
		}

		public void Register<TComponent, TModel>(TComponent component)
			where TComponent : Component<TModel>
			where TModel : Model
		{
			_assets.AddRange(component.GetAssets());
			_jsx.Add(component.GetType().Name);
		}

		public string Render()
		{
			var sb = new StringBuilder();

			sb.AppendLine("<html>");
			sb.AppendLine("<head>");

			sb.AppendLine("</head>");
			sb.AppendLine("<body>");

			_jsx.ForEach(name =>
			{
				sb.AppendFormat("<script type=\"text/jsx\" src=\"components/{0}\"></script>", name);
				sb.AppendLine();
			});

			sb.AppendLine("</body>");
			sb.AppendLine("</html>");

			return sb.ToString();
		}

	}
}
