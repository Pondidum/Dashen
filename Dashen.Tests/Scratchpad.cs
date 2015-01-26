using System;
using System.Threading;
using Dashen.Components;
using Dashen.Content;
using Newtonsoft.Json;
using Shouldly;
using StructureMap;
using Xunit;

namespace Dashen.Tests
{
	public class Scratchpad
	{
		[Fact]
		public void When_running_the_dashboard()
		{

			var container = new Container(config =>
			{
				config.Scan(a =>
				{
					a.AssemblyContainingType<Dashboard>();
					a.WithDefaultConventions();
				});

				config.For<View>().Singleton();
				config.For<ModelRepository>().Singleton();
				config.For<ComponentRepository>().Singleton();
			});

			var dashboard = container.GetInstance<Dashboard>();
			dashboard.Add<TextComponent, TextModel>(model => model.Text = "Testing");
			dashboard.Start().Wait();

			while (true)
			{
				Thread.Sleep(1000);
			}
		}
	}



	
}
