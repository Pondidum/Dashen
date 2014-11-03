using System;
using Spark;

namespace Dashen.Infrastructure.Spark
{
	public abstract class DashenView : SparkViewBase
	{
		public ApplicationModel App { get; set; }
		public DashenConfiguration Configuration { get; set; }

		public string Route(string route)
		{
			return Configuration.ApplyPrefix(route);
		}
	}

	public class DashenView<TViewModel> : DashenView, IDashenView where TViewModel : class
	{
		public TViewModel Model { get; set; }

		public void SetModel(object model)
		{
			Model = (TViewModel)model;
		}
		
		public override void Render()
		{
			RenderView(Output);
		}

		public override Guid GeneratedViewId
		{
			get { return Guid.NewGuid(); }
		}
	}
}
