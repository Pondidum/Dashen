using System;
using Dashen.Models;

namespace Dashen
{
	public class Definition
	{
		public Func<ControlViewModel> Create { get; set; }
		public string Heading { get; set; }
	}
}
