using Dashen.Controls;
using Dashen.Infrastructure;

namespace Dashen.Endpoints.Stats
{
	public class StatsViewModelBuilder
	{
		private readonly DashenConfiguration _config;

		public StatsViewModelBuilder(DashenConfiguration config)
		{
			_config = config;
		}

		internal WidgetModel FromWidget(Widget widget)
		{
			var model = widget.Create();

			model.ID = widget.ID;
			model.UpdateUrl = RouteHelper.For<WidgetsController>(_config, c => c.Update("")) + widget.ID;
			model.Interval = (int)widget.Interval.TotalMilliseconds;

			return model;
		}
	}
}
