using System.Collections.Generic;

namespace Dashen.Models
{
	public class IndexViewModel
	{
		public IEnumerable<DefinitionModel> Definitions { get; set; }
	}

	public class DefinitionModel
	{
		public string ID { get; set; }
		public string Url { get; set; }
		public string Heading { get; set; }
	}
}
