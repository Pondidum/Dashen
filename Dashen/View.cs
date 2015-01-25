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
			_jsx.Add(component.GetJsx());
		}

		public string Render()
		{
			var sb = new StringBuilder();

			sb.AppendLine("<html>");
			sb.AppendLine("<body>");

			sb.AppendLine("<script type=\"text\\jsx\">");
			_jsx.ForEach(jsx => sb.AppendLine(jsx));
			sb.AppendLine("</script>");

			sb.AppendLine("</body>");
			sb.AppendLine("</html>");

			return sb.ToString();
		}

	}
}
