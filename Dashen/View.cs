using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dashen.Infrastructure;

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

		public void AddAsset(AssetInfo asset)
		{
			_assets.Add(asset);
		}

		private void Include(StringBuilder sb, AssetLocations location)
		{
			_assets
				.Where(asset => asset.Location == location)
				.ForEach(asset => sb.AppendLine(asset.ToString()));
		}

		public string Render()
		{
			var sb = new StringBuilder();

			sb.AppendLine("<html>");
			sb.AppendLine("<head>");

			Include(sb, AssetLocations.PreHead);
			Include(sb, AssetLocations.PostHead);

			sb.AppendLine("</head>");
			sb.AppendLine("<body>");

			Include(sb, AssetLocations.PreBody);
			Include(sb, AssetLocations.PostBody);

			sb.AppendLine("</body>");
			sb.AppendLine("</html>");

			return sb.ToString();
		}

	}
}
