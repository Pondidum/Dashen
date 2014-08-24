using System.Net;
using Dashen.Controllers;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Controllers
{
	public class StaticControllerTests
	{
		private readonly StaticController _controller;

		public StaticControllerTests()
		{
			_controller = new StaticController(new MimeLookup());
		}

		[Fact]
		public void When_a_valid_file_is_requested()
		{
			var response = _controller.GetDispatch("css/style.css");

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public void When_a_non_existing_file_is_requested()
		{
			var response = _controller.GetDispatch("wefw/wefwefwefwefwef.css");

			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
