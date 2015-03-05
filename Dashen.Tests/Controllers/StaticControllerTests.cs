using System.Collections.Generic;
using System.Net;
using System.Text;
using Dashen.Controllers;
using Dashen.Static;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Controllers
{
	public class StaticControllerTests
	{
		private readonly StaticController _controller;
		private List<Resource> _resources;

		public StaticControllerTests()
		{
			_resources = new List<Resource>();

			var config = new DashboardConfiguration { Resources = _resources };
			var userContent = new UserContentProvider(config);
			var embeddedContent = new StaticContentProvider();

			_controller = new StaticController(userContent, embeddedContent);
		}

		[Fact]
		public void When_no_resource_matches()
		{
			_controller
				.Get("test", "resource.png")
				.StatusCode
				.ShouldBe(HttpStatusCode.NotFound);
		}

		[Fact]
		public void When_a_resource_exists_in_user_defined()
		{
			_resources.Add(new Resource("test.txt", "text/plain", Encoding.UTF8.GetBytes("Testing content")));
			_controller
				.Get("user", "test.txt")
				.StatusCode
				.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public void When_a_resource_exists_in_embedded()
		{
			_controller
				.Get("js", "footer.jsx")
				.StatusCode
				.ShouldBe(HttpStatusCode.OK);
		}

	}
}
