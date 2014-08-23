using System;
using Spark;

namespace Dashen.Infrastructure.Spark
{
	public abstract class DashenView : SparkViewBase
	{
	}

	public class DashenView<TViewModel> : DashenView where TViewModel : class
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

		public override bool TryGetViewData(string name, out object value)
		{
			return base.TryGetViewData(name, out value);
		}
	}
}
