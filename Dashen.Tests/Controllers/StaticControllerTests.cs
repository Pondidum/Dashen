using System.Net;
using Dashen.Endpoints.Static;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Controllers
{
	public class StaticControllerTests
	{
		private readonly StaticController _controller;

		public StaticControllerTests()
		{
			var content = new  EmbeddedStaticContentProvider( new MimeLookup());

			_controller = new StaticController(new[] { content });
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
