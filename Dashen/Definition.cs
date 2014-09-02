using System;
using Dashen.Endpoints.Stats;

namespace Dashen
{
	public class Definition
	{
		private int _width;
		public Func<ControlViewModel> Create { get; set; }
		public string Heading { get; set; }
		public TimeSpan Interval { get; set; }

		public int Width
		{
			get { return _width; }
			set
			{
				if (value <= 0 || value > 12)
				{
					throw new ArgumentOutOfRangeException("value", string.Format("Was {0}, it must be between 1 and 12.", value));
				}

				_width = value;
			}
		}

		public Definition()
		{
			Interval = new TimeSpan(0, 0, 10);
			Width = 4;
		}

		public string ID
		{
			get { return Heading.Replace(" ", ""); }
		}
	}
}
