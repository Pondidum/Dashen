using System.Collections.Generic;

namespace Dashen.Models
{
	public class GraphControlViewModel : ControlViewModel
	{
		public IEnumerable<KeyValuePair<int, int>> Points { get; set; }
		public IEnumerable<KeyValuePair<int, string>> XTicks { get; set; }
	}
}
