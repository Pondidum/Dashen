using Dashen.Endpoints.Index;
using Dashen.Endpoints.Stats;

namespace Dashen
{
	public class DefinitionModelBuilder
	{
		internal ControlViewModel BuildStatsViewModel(Widget definition)
		{
			var model = definition.Create();

			model.ID = definition.ID;
			model.UpdateUrl = "stats/update/" + definition.ID;
			model.Interval = (int)definition.Interval.TotalMilliseconds;

			return model;
		}
	}
}
