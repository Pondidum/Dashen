using Dashen.Endpoints.Static;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Endpoints.Static
{
	public class ReplacementSourceTests
	{
		[Fact]
		public void When_no_replacements_have_been_added()
		{
			var rs = new ReplacementSource();

			rs.Process("this is a !{test}.").ShouldBe("this is a !{test}.");
		}

		[Fact]
		public void When_a_replacement_has_been_added()
		{
			var rs = new ReplacementSource();
			rs.Add("test", "win");
			rs.Process("this is a !{test}.").ShouldBe("this is a win.");
		}
	}
}
