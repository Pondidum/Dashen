using System;
using System.Diagnostics;
using System.Linq;
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
				ListenOn = new Uri("http://localhost:3030"),
				ApplicationName =  "UnitTestRunner",
				ApplicationVersion = "1.3.3.7"
			});

			dashboard.Add<TextModel>(model =>
			{
				model.Title = "Header";
				model.Text = "Testing";
			});

			var counter = 0;

			dashboard.Add<TextModel>(model =>
			{
				model.Title = "Header";
				model.Text = counter.ToString();
			});

			dashboard.Add<HtmlModel>(model =>
			{
				model.Title = "Html";
				model.Html = "<b>Test Bold</b>";
			});

			dashboard.Add<ListModel>(model =>
			{
				model.Title = "Listy";
				model.Items = new[] {"First", "Second", "Third"}.ToList();
			});

			dashboard.Start().Wait();

			while (true)
			{
				Thread.Sleep(1000);
				counter++;
			}
		}
	}



	
}
