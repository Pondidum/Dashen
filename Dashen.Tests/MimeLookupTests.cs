﻿using Shouldly;
using Xunit;

namespace Dashen.Tests
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
				.MediaType
				.ShouldBe("text/css");
		}

		[Fact]
		public void When_the_file_extension_is_js()
		{
			new MimeLookup()
				.Get(".js")
				.MediaType
				.ShouldBe("text/javascript");
		}
	}
}