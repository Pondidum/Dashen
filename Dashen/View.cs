using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dashen.Assets;
using Dashen.Infrastructure;

namespace Dashen
{
	public class View
	{
		private readonly List<AssetInfo> _assets;

		public View()
		{
			_assets = new List<AssetInfo>();

			_assets.Add(new JavaScriptAssetInfo("static/js/react.min.js"));
			_assets.Add(new JavaScriptAssetInfo("static/js/JSXTransformer.js"));
		}

		public void AddAsset(AssetInfo asset)
		{
			_assets.Add(asset);
		}


		private string GetTemplate()
		{
			using (var stream = GetType().Assembly.GetManifestResourceStream("Dashen.Views.Index.htm"))
			using (var reader = new StreamReader(stream))
			{
				return reader.ReadToEnd();
			}
		}

		private string GetScripts()
		{
			var sb = new StringBuilder();

			_assets
				.Where(asset => asset.Location == AssetLocations.PreHead || asset.Location == AssetLocations.PostHead)
				.ForEach(asset => sb.AppendLine(asset.ToString()));

			return sb.ToString();
		}

		private string GetComponents()
		{
			var sb = new StringBuilder();

			_assets
				.Where(asset => asset.Location == AssetLocations.PreBody || asset.Location == AssetLocations.PostBody)
				.ForEach(asset => sb.AppendLine(asset.ToString()));

			return sb.ToString();
		}

		private string BuildReactUI()
		{
			var start = @"
var Dashboard = React.createClass({
  render: function() {
    return (
      <div className='row fullwidth'>
";
			var finish = @"
      </div>
    );
  }
});";
			var sb = new StringBuilder();
			sb.AppendLine(start);

			_assets
				.OfType<ComponentAssetInfo>()
				.Select(c => c.Name)
				.ForEach(name => sb.AppendFormat("        <{0} />", name));

			sb.AppendLine(finish);

			return sb.ToString();

		}

		public string Render()
		{
			var template = GetTemplate();


			template = template.Replace("{scripts}", GetScripts());
			template = template.Replace("{components}", GetComponents());

			template = template.Replace("{generated}", BuildReactUI());

			return template;
		}

	}
}
