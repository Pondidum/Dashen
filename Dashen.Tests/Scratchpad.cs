using System;
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

			var dashboard = DashboardBuilder.Create(new DashboardConfiguration
			{
				ListenOn = new Uri("http://localhost:3030")
			});

			dashboard.Add<TextComponent, TextModel>(model =>
			{
				model.Title = "Header";
				model.Text = "Testing";
			});

			dashboard.Start().Wait();

			while (true)
			{
				Thread.Sleep(1000);
			}
		}
	}



	
}
