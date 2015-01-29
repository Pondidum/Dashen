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
			_assets.Add(new JavaScriptAssetInfo("static/js/jquery-1.10.0.min.js"));
			_assets.Add(new JavaScriptAssetInfo("static/js/wrapper.jsx"));
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
			var dashboardJsx = @"
var Dashboard = React.createClass({
  render: function() {
    return (
      <div className='row fullwidth'>
        {components}
      </div>
    );
  }
});

React.renderComponent(
  <Dashboard />,
  document.getElementById('content')
);";

			var componentFormat = "        <Wrapper component={{{0}}} url='{1}' interval={{{2}}} />";

			var sb = new StringBuilder();

			_assets
				.OfType<ComponentAssetInfo>()
				.ForEach(component => sb.AppendFormat(componentFormat, component.Name, component.ModelPath, 5000 ));

			return dashboardJsx.Replace("{components}", sb.ToString());

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
