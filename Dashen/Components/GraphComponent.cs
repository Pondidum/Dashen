using System.IO;
using System.Text;
using Dashen.Static;

namespace Dashen.Components
{
	public class GraphComponent : Component<GraphModel>
	{
		private readonly string _jsx;

		public GraphComponent(StaticContentProvider content)
		{
			var resource = content.GetContent("js", "graphComponent.jsx");

			_jsx = Encoding.UTF8.GetString(resource.Content);
		}

		public override string GetJsx()
		{
			return _jsx;
		}
	}
}
