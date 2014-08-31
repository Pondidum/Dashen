using System;
using Dashen.Models;

namespace Dashen
{
	public class Definition
	{
		public Func<ControlViewModel> Create { get; set; }
		public string Heading { get; set; }
		public TimeSpan Interval { get; set; }

		public Definition()
		{
			Interval = new TimeSpan(0, 0, 10);
		}

		public string ID
		{
			get { return Heading.Replace(" ", ""); }
		}

		internal ControlViewModel BuildStatsViewModel()
		{
			var model = Create();
			model.Name = ID;

			return model;
		}

		internal IndexDisplayViewModel BuildIndexDisplayViewModel()
		{
			var model = new IndexDisplayViewModel
			{
				Heading = Heading,
				ID = ID,
				Url = "stats/" + ID,
				Interval = (int)Interval.TotalMilliseconds,
				Columns = 4,
			};

			return model;
		}
	}
}
