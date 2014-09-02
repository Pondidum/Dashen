using Dashen.Endpoints.Index;
using Dashen.Endpoints.Stats;

namespace Dashen
{
	public class DefinitionModelBuilder
	{
		internal ControlViewModel BuildStatsViewModel(Definition definition)
		{
			var model = definition.Create();
			model.Name = definition.ID;

			return model;
		}

		internal IndexDisplayViewModel BuildIndexDisplayViewModel(Definition definition)
		{
			var model = new IndexDisplayViewModel
			{
				Heading = definition.Heading,
				ID = definition.ID,
				Url = "stats/" + definition.ID,
				Interval = (int)definition.Interval.TotalMilliseconds,
				Columns = definition.Width,
			};

			return model;
		} 
	}
}
