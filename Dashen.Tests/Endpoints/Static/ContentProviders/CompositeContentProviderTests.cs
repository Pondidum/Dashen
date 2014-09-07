using System;
using System.IO;
using System.Linq;
using Dashen.Endpoints.Static;
using Dashen.Endpoints.Static.ContentProviders;
using NSubstitute;
using NSubstitute.Core.Arguments;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Endpoints.Static.ContentProviders
{
	public class CompositeContentProviderTests
	{
		private IStaticContentProvider WithContent(string urlFragment)
		{
			var provider = Substitute.For<IStaticContentProvider>();

			provider
				.GetContent(urlFragment)
				.Returns(new StaticContent { Stream = new MemoryStream(), MimeType = "text/test" });

			return provider;
		}

		private IStaticContentProvider WithoutContent(string urlFragment)
		{
			var provider = Substitute.For<IStaticContentProvider>();

			provider
				.GetContent(urlFragment)
				.Returns((StaticContent)null);

			return provider;
		}

		private StaticContent RunTest(params IStaticContentProvider[] providers)
		{
			var composite = new CompositeContentProvider(providers);

			return composite.GetContent("test");
		}

		[Fact]
		public void When_the_first_source_has_content()
		{
			var first = WithContent("test");
			var second = WithoutContent("test");

			var content = RunTest(first, second);

			content.ShouldNotBe(null);

			first.Received().GetContent("test");
			second.DidNotReceive().GetContent("test");
		}

		[Fact]
		public void When_the_second_source_has_content()
		{
			var first = WithoutContent("test");
			var second = WithContent("test");

			var content = RunTest(first, second);

			content.ShouldNotBe(null);

			first.Received().GetContent("test");
			second.Received().GetContent("test");
		}

		[Fact]
		public void When_neither_source_has_content()
		{
			var first = WithoutContent("test");
			var second = WithoutContent("test");

			var content = RunTest(first, second);

			content.ShouldBe(null);

			first.Received().GetContent("test");
			second.Received().GetContent("test");
		}

		[Fact]
		public void When_both_sources_have_content()
		{
			var first = WithContent("test");
			var second = WithContent("test");

			var content = RunTest(first, second);

			content.ShouldNotBe(null);

			first.Received().GetContent("test");
			second.DidNotReceive().GetContent("test");
		}
	}
}
