using System.Collections.Generic;
using System.Linq;

namespace Dashen.Endpoints.Stats
{
	public class GraphControlViewModel : ControlViewModel
	{
		public IEnumerable<KeyValuePair<int, int>> Points { get; set; }
		public IEnumerable<KeyValuePair<int, string>> XTicks { get; set; }
		public IEnumerable<KeyValuePair<int, string>> YTicks { get; set; }

		public GraphControlViewModel()
		{
			Points = Enumerable.Empty<KeyValuePair<int, int>>();
			XTicks = Enumerable.Empty<KeyValuePair<int, string>>();
			YTicks = Enumerable.Empty<KeyValuePair<int, string>>();
		}
	}
}
