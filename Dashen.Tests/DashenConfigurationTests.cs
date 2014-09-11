using System.Linq;
using Dashen.Infrastructure;
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
	}
}
