using System;
using System.IO;
using Dashen.Endpoints.Static.ContentProviders;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Endpoints.Static.ContentProviders
{
	public class AdhocContentProviderTests
	{
		[Fact]
		public void When_adding_content_with_existing_fragment()
		{
			var adhoc = new AdhocContentProvider();

			adhoc.Add("test", new MemoryStream(), "test/content");

			Should.Throw<Exception>(() => adhoc.Add("test", new MemoryStream(), "test/content"));
		}

		[Fact]
		public void When_getting_content_it_can_be_read_multiple_times()
		{
			var adhoc = new AdhocContentProvider();

			adhoc.Add("test", StreamFromContent("Test content"), "test/content");

			var content1 = ReadToEnd(adhoc.GetContent("test").Stream);
			var content2 = ReadToEnd(adhoc.GetContent("test").Stream);

			content1.ShouldBe("Test content");
			content2.ShouldBe("Test content");
		}

		private Stream StreamFromContent(string content)
		{
			var ms = new MemoryStream();
			var sw = new StreamWriter(ms);

			sw.Write(content);
			sw.Flush();
			ms.Position = 0;

			return ms;
		}

		private string ReadToEnd(Stream stream)
		{
			using (var sr = new StreamReader(stream))
			{
				return sr.ReadToEnd();
			}
		}
	}
}
