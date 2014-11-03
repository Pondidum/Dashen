using Dashen.Endpoints.Stats;
using Dashen.Infrastructure;

namespace Dashen.Endpoints.Index
{
	public class IndexViewModelBuilder
	{
		private readonly DashenConfiguration _config;

		public IndexViewModelBuilder(DashenConfiguration config)
		{
			_config = config;
		}

		internal IndexDisplayViewModel FromWidget(Widget definition)
		{
			var model = new IndexDisplayViewModel
			{
				Heading = definition.Heading,
				ID = definition.ID,
				CreateWidgetUrl = RouteHelper.For<WidgetsController>(_config, c => c.CreateWidget("")) + definition.ID,
				Columns = definition.Width,
			};

			return model;
		}
	}
}
