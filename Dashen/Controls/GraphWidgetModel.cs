using System.Collections.Generic;
using System.Linq;
using Dashen.Endpoints.Stats;

namespace Dashen.Controls
{
	public class GraphWidgetModel : WidgetModel
	{
		public IEnumerable<Pair> Points { get; set; }
		public IEnumerable<Label> XTicks { get; set; }
		public IEnumerable<Label> YTicks { get; set; }

		public GraphWidgetModel()
		{
			Points = Enumerable.Empty<Pair>();
			XTicks = Enumerable.Empty<Label>();
			YTicks = Enumerable.Empty<Label>();
		}
	}
}
