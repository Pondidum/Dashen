using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dashen;
using Dashen.Endpoints.Stats;

namespace ConsoleTestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var ui = new Dashboard(new DashenConfiguration{
				ListenOn = new Uri("http://localhost:8080")
			});

			var model = new TextControlViewModel { Content = "Test" };
			//config all the things...
			ui.Register(new Widget
			{
				Create = () => model,
				Heading = "Text",
				Interval = new TimeSpan(0,0,1),
				Width = 2,
			});

			ui.Register(new Widget
			{
				Create = () => new ListControlViewModel { Items = new[] { "One", "Two", "Many", "Lots" }.ToList() },
				Heading = "List",
				Width = 3
			});

			ui.Register(new Widget
			{
				Create = () => new GraphControlViewModel
				{
					Points = new[] { new KeyValuePair<int, int>(0,10), new KeyValuePair<int, int>(50, 15) },
					XTicks = new[] { new KeyValuePair<int, string>(0, "left"), new KeyValuePair<int, string>(50, "right") }
				},
				Heading = "Graph",
				Width = 7
			});

			ui.Register(new Widget
			{
				Create = () => new BarGraphControlViewModel
				{
					Points = new[] { new KeyValuePair<int, int>(0, 10), new KeyValuePair<int, int>(2, 15) },
					XTicks = new[] { new KeyValuePair<int, string>(0, "left"), new KeyValuePair<int, string>(2, "right") }
				},
				Heading = "Bar",
				Width = 3
			});

			ui.Start();

			Console.WriteLine("Webui running on port 8080.");
			Console.WriteLine("Press any key to exit.");

			Task.Run(() =>
			{
				var counter = 0;
				while (true)
				{
					counter++;
					model.Content = "Test " + counter;

					Thread.Sleep(1000);
				}
			});

			Console.ReadKey();
		}
	}
}
