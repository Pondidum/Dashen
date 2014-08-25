﻿using System;
using Dashen.Models;

namespace Dashen
{
	public class Definition
	{
		public Func<ControlViewModel> Create { get; set; }
		public string Heading { get; set; }
		public string Name { get; set; }
		public TimeSpan Interval { get; set; }

		public Definition()
		{
			Interval = new TimeSpan(0, 0, 10);
		}
	}
}
