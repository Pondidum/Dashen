using System;
using Dashen;

namespace ConsoleTestApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var ui = new Dashboard(new Uri("http://localhost:8080"));

			//config all the things...
			//ui.

			ui.Start();

			Console.WriteLine("Webui running on port 8080.");
			Console.WriteLine("Press any key to exit.");

			Console.ReadKey(); 
		}
	}
}
