using System.Collections.Generic;
using Dashen.Infrastructure;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Infrastructure.Extensions
{
	public class GetTests
	{

		[Fact]
		public void When_the_key_exists()
		{
			var dictionary = new Dictionary<string, int>();
			dictionary["a"] = 1;
			
			dictionary
				.Get("a")
				.ShouldBe(1);
		}

		[Fact]
		public void When_the_key_doesnt_exist_and_value_is_a_value_type()
		{
			var dictionary = new Dictionary<string, int>();
			dictionary["a"] = 1;

			dictionary
				.Get("r")
				.ShouldBe(0);
		}

		[Fact]
		public void When_the_key_doesnt_exist_and_value_is_a_reference_type()
		{
			var dictionary = new Dictionary<string, FactAttribute>();
			dictionary["a"] = new FactAttribute();

			dictionary
				.Get("r")
				.ShouldBe(null);
		}
	}
}
