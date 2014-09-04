using System;
using System.Drawing;

namespace Dashen
{
	public class DashenConfiguration
	{
		public Uri ListenOn { get; set; }

		public Color HighlightColor { get; set; }
		public Color LowlightColor { get; set; }

		public DashenConfiguration()
		{
			ListenOn = new Uri("http://localhost:8080");
			HighlightColor = ColorTranslator.FromHtml("#33CC33");
			LowlightColor = ColorTranslator.FromHtml("#248F24");
		}
	}
}
