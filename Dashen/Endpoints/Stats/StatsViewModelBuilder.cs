using Dashen.Controls;

namespace Dashen.Endpoints.Stats
{
	public class StatsViewModelBuilder
	{
		internal ControlViewModel FromWidget(Widget widget)
		{
			var model = widget.Create();

			model.ID = widget.ID;
			model.UpdateUrl = "stats/update/" + widget.ID;
			model.Interval = (int)widget.Interval.TotalMilliseconds;

			return model;
		}
	}
}
