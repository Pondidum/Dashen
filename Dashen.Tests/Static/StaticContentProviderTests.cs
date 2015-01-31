using System.IO;
using Dashen.Static;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Static
{
	public class StaticContentProviderTests
	{
		private readonly StaticContentProvider _provider;

		public StaticContentProviderTests()
		{
			_provider = new StaticContentProvider();
		}

		[Fact]
		public void When_fetching_non_existing_file_from_a_valid_directory()
		{
			_provider
				.GetContent("js", "rrrrrrrrrrrrr.js")
				.ShouldBe(Stream.Null);
		}

		[Fact]
		public void When_fetching_a_file_from_a_non_existing_directory()
		{
			_provider
				.GetContent("rrrrrrrrr", "style.css")
				.ShouldBe(Stream.Null);
		}

		[Fact]
		public void When_fetching_an_existing_resource()
		{
			_provider
				.GetContent("css", "style.css")
				.Length
				.ShouldBeGreaterThan(0);
		}

		[Fact]
		public void When_fetching_from_a_different_case_directory()
		{
			_provider
				.GetContent("CSS", "style.css")
				.Length
				.ShouldBeGreaterThan(0);
		}
	}
}
