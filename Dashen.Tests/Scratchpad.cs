using System.Diagnostics;
using System.Threading;
using Dashen.Components;
using Xunit;

namespace Dashen.Tests
{
	public class Scratchpad
	{
		[Fact]
		public void When_running_the_dashboard()
		{
			if (Debugger.IsAttached == false)
			{
				return;
			}

			var dashboard = Dashboard.Create();

			dashboard.Add<TextComponent, TextModel>(model => model.Text = "Testing");
			dashboard.Start().Wait();

			while (true)
			{
				Thread.Sleep(1000);
			}
		}
	}



	
}
