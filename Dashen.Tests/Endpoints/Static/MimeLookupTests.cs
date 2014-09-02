using Dashen.Endpoints.Static;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Endpoints.Static
{
	public class MimeLookupTests
	{
		[Fact]
		public void When_there_is_no_mime_type_for_the_file()
		{
			new MimeLookup()
				.Get(".txt")
				.ShouldBe(null);
		}

		[Fact]
		public void When_the_file_extension_is_css()
		{
			new MimeLookup()
				.Get(".css")
				.ShouldBe("text/css");
		}

		[Fact]
		public void When_the_file_extension_is_js()
		{
			new MimeLookup()
				.Get(".js")
				.ShouldBe("text/javascript");
		}
	}
}
