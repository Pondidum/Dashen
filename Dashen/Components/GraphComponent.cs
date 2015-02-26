using System.IO;
using Dashen.Static;

namespace Dashen.Components
{
	public class GraphComponent : Component<GraphModel>
	{
		private readonly string _jsx;

		public GraphComponent(StaticContentProvider content)
		{
			using (var stream = content.GetContent("js", "graphComponent.jsx"))
			using (var sr = new StreamReader(stream))
			{
				_jsx = sr.ReadToEnd();
			}
		}

		public override string GetJsx()
		{
			return _jsx;
		}
	}
}
