using System.Collections.Generic;
using System.Linq;

namespace Dashen.Components
{
	public class GraphModel : Model
	{
		public IEnumerable<Pair> Points { get; set; }
		public IEnumerable<Label> XTicks { get; set; }
		public IEnumerable<Label> YTicks { get; set; } 

		public GraphModel()
		{
			Points = Enumerable.Empty<Pair>();
			XTicks = Enumerable.Empty<Label>();
			YTicks = Enumerable.Empty<Label>();
		}
	}
}
