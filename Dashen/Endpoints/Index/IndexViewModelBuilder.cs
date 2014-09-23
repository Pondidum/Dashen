using Dashen.Endpoints.Stats;
using Dashen.Infrastructure;

namespace Dashen.Endpoints.Index
{
	public class IndexViewModelBuilder
	{
		internal IndexDisplayViewModel FromWidget(Widget definition)
		{
			var model = new IndexDisplayViewModel
			{
				Heading = definition.Heading,
				ID = definition.ID,
				CreateWidgetUrl = RouteHelper.For<StatsController>(c => c.CreateWidget("")) + definition.ID,
				Columns = definition.Width,
			};

			return model;
		}
	}
}
