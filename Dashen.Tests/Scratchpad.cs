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

			dashboard.Add<GraphModel>(model =>
			{
				model.Title = "Graphy";
				model.Columns = 8;
				model.Points = new[] {new Pair(1, 1), new Pair(2, 10), new Pair(3, 6), new Pair(4, 2), new Pair(5, 3)};
				model.XTicks = new[] {new Label(1, "Left"), new Label(3, "Middle"), new Label(5, "Right")};
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
