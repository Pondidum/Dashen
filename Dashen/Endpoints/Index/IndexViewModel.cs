using System.Collections.Generic;
using System.Linq;

namespace Dashen.Endpoints.Index
{
	public class IndexViewModel
	{
		public IEnumerable<IndexDisplayViewModel> Definitions { get; set; }

		public IndexViewModel()
		{
			Definitions = Enumerable.Empty<IndexDisplayViewModel>();
		}
	}
}
