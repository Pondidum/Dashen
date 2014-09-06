using System.IO;
using Dashen.Endpoints.Static;
using NSubstitute;
using Xunit;

namespace Dashen.Tests.Endpoints.Static
{
	public class CachingContentProviderTests
	{
		[Fact]
		public void A_url_fragment_only_hits_the_wapped_resource_once()
		{
			var content = new StaticContent { Stream = new MemoryStream(), MimeType = "testmime" };
			var wrapped = Substitute.For<IStaticContentProvider>();
			wrapped.GetContent("test").Returns(content);

			var cache = new CachingContentProvider(wrapped);

			cache.GetContent("test");
			cache.GetContent("test");

			wrapped.Received(1).GetContent("test");
		}

		[Fact]
		public void A_url_fragment_which_doesnt_have_content_only_hits_wrapped_resource_once()
		{
			var wrapped = Substitute.For<IStaticContentProvider>();
			wrapped.GetContent("test").Returns((StaticContent)null);

			var cache = new CachingContentProvider(wrapped);

			cache.GetContent("test");
			cache.GetContent("test");

			wrapped.Received(1).GetContent("test");
		}
	}
}
