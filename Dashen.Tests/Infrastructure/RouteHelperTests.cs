﻿using System.Net.Http;
using System.Web.Http;
using Dashen.Infrastructure;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Infrastructure
{
	public class RouteHelperTests
	{
		[Fact]
		public void Generating_a_route_gives_a_correct_result()
		{
			var config = new DashenConfiguration();
			config.Prefix = "";

			var route = RouteHelper.For<TestController>(config, c => c.DoSomething());

			route.ShouldBe("Test/DoSomething/");
		}

		private class TestController : ApiController
		{
			public HttpResponseMessage DoSomething()
			{
				return null;
			}
		}
	}
}