﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dashen;
using Dashen.Controls;
using Dashen.Endpoints.Stats;

namespace ConsoleTestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var config = new DashenConfiguration
			{
				ListenOn = new Uri("http://localhost:8080")
			};

			config.EnableConsoleLog();

			var ui = new Dashboard(config);

			var model = new TextControlViewModel { Content = "Test" };
			//config all the things...
			ui.Register(new Widget
			{
				Create = () => model,
				Heading = "Text",
				Interval = new TimeSpan(0, 0, 10),
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
					Points = new[] { new Pair(0,10), new Pair(50, 15) },
					XTicks = new[] { new Label(0, "left"), new Label(50, "right") }
				},
				Heading = "Graph",
				Width = 7
			});

			ui.Register(new Widget
			{
				Create = () => new BarGraphControlViewModel
				{
					Points = new[] { new Pair(0, 10), new Pair(2, 15) },
					XTicks = new[] { new Label(0, "left"), new Label(2, "right") }
				},
				Heading = "Bar",
				Width = 3
			});

			ui.Register(new Widget
			{
				Create = () => new ProgressControlViewModel { Percentage = 0.75M},
				Heading = "Prg"
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
