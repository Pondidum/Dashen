using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dashen;
using Dashen.Models;

namespace ConsoleTestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var ui = new Dashboard(new Uri("http://localhost:8080"));

			var model = new TextControlViewModel { Content = "Test" };
			//config all the things...
			ui.RegisterModel(new Definition
			{
				Create = () => model,
				Heading = "Some Text",
				Name = "TestContent"
			});

			ui.RegisterModel(new Definition
			{
				Create = () => new ListControlViewModel { Items = new[] { "One", "Two", "Many", "Lots" }.ToList() },
				Heading = "Four things",
				Name = "FourThings"
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
