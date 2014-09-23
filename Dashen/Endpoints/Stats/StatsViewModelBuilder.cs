using Dashen.Controls;
using Dashen.Infrastructure;

namespace Dashen.Endpoints.Stats
{
	public class StatsViewModelBuilder
	{
		internal WidgetModel FromWidget(Widget widget)
		{
			var model = widget.Create();

			model.ID = widget.ID;
			model.UpdateUrl = RouteHelper.For<StatsController>(c => c.Update("")) + widget.ID;
			model.Interval = (int)widget.Interval.TotalMilliseconds;

			return model;
		}
	}
}
