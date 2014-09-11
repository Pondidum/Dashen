using System.IO;
using System.Linq;
using System.Text;
using Dashen.Endpoints.Static;
using Dashen.Endpoints.Static.ContentProviders;
using Dashen.Infrastructure;
using Dashen.Initialisation;
using Shouldly;
using Xunit;

namespace Dashen.Tests
{
	public class DashenConfigurationTests
	{
		[Fact]
		public void EnableConsoleLog_adds_a_logger_to_the_handlers_if_one_is_not_present()
		{
			var config = new DashenConfiguration();

			config.EnableConsoleLog();

			config.MessageHandlers.Single().ShouldBeOfType<ConsoleLoggingHandler>();
		}

		[Fact]
		public void EnableConsoleLog_doesnt_add_a_logger_if_handlers_contains_a_console_logger_already()
		{
			var config = new DashenConfiguration();
			config.MessageHandlers.Add(new ConsoleLoggingHandler());

			config.EnableConsoleLog();

			config.MessageHandlers.Count.ShouldBe(1);
			config.MessageHandlers.Single().ShouldBeOfType<ConsoleLoggingHandler>();
		}

		[Fact]
		public void DisableConsoleLog_removes_the_logger_if_it_exists()
		{
			var config = new DashenConfiguration();
			config.MessageHandlers.Add(new ConsoleLoggingHandler());

			config.DisableConsoleLog();

			config.MessageHandlers.ShouldBeEmpty();
		}

		[Fact]
		public void DisableConsoleLog_does_nothing_if_there_is_no_logger_already()
		{
			var config = new DashenConfiguration();

			config.DisableConsoleLog();

			config.MessageHandlers.ShouldBeEmpty();
		}

		[Fact]
		public void AddResource_allows_the_content_stream_to_be_shut()
		{
			var config = new DashenConfiguration();

			using (var ms = new MemoryStream())
			using( var writer = new StreamWriter(ms))
			{
				writer.Write("Test value");
				writer.Flush();
				ms.Position = 0;

				config.AddResource("test", ms, "text/plain");
			}
			var adhoc = new AdhocContentProvider();
			var init = new StaticContentInitialisation(new ReplacementSource(), adhoc);
			init.ApplyTo(config, null);

			using (var reader = new StreamReader(adhoc.GetContent("test").Stream))
			{
				reader.ReadToEnd().ShouldBe("Test value");
			}
		}
	}
}
