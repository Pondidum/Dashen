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

		internal IndexDisplayViewModel BuildIndexDisplayViewModel(Widget definition)
		{
			var model = new IndexDisplayViewModel
			{
				Heading = definition.Heading,
				ID = definition.ID,
				CreateWidgetUrl = "stats/createWidget/" + definition.ID,
				Columns = definition.Width,
			};

			return model;
		} 
	}
}
