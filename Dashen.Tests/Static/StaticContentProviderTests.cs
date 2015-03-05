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
				.GetResource("js", "rrrrrrrrrrrrr.js")
				.ShouldBe(Resource.Empty);
		}

		[Fact]
		public void When_fetching_a_file_from_a_non_existing_directory()
		{
			_provider
				.GetResource("rrrrrrrrr", "style.css")
				.ShouldBe(Resource.Empty);
		}

		[Fact]
		public void When_fetching_an_existing_resource()
		{
			_provider
				.GetResource("css", "style.css")
				.Content
				.Length
				.ShouldBeGreaterThan(0);
		}

		[Fact]
		public void When_fetching_from_a_different_case_directory()
		{
			_provider
				.GetResource("CSS", "style.css")
				.Content
				.Length
				.ShouldBeGreaterThan(0);
		}
	}
}
