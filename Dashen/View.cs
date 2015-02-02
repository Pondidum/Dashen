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

			var componentFormat = "        <Wrapper component={{{0}}} url='models/{1}' interval={{{2}}} />";

			var sb = new StringBuilder();

			_modelInfo.All().ForEach(info =>
			{
				sb.AppendFormat(componentFormat, info.Component.Name, info.ModelID, 5000);
				sb.AppendLine();
			});

			return dashboardJsx.Replace("{components}", sb.ToString());

		}

		public string Render()
		{
			var template = GetTemplate();


			template = template.Replace("{components}", GetComponents());
			template = template.Replace("{generated}", BuildReactUI());

			return template;
		}

	}
}
