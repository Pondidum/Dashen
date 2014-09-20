using System.Collections.Generic;
using System.Linq;

namespace Dashen.Endpoints.Stats
{
	public class GraphControlViewModel : ControlViewModel
	{
		public IEnumerable<Pair> Points { get; set; }
		public IEnumerable<Label> XTicks { get; set; }
		public IEnumerable<Label> YTicks { get; set; }

		public GraphControlViewModel()
		{
			Points = Enumerable.Empty<Pair>();
			XTicks = Enumerable.Empty<Label>();
			YTicks = Enumerable.Empty<Label>();
		}
	}
}
