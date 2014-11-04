using System.Net;
using Dashen.Endpoints.Static;
using Dashen.Endpoints.Static.ContentProviders;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Endpoints.Static
{
	public class StaticControllerTests
	{
		private readonly StaticController _controller;

		public StaticControllerTests()
		{
			var content = new  EmbeddedStaticContentProvider( new MimeLookup());

			_controller = new StaticController(content);
		}

		[Fact]
		public void When_a_valid_file_is_requested()
		{
			var response = _controller.GetDispatch("css/style");

			response.StatusCode.ShouldBe(HttpStatusCode.OK);
		}

		[Fact]
		public void When_a_non_existing_file_is_requested()
		{
			var response = _controller.GetDispatch("wefw/wefwefwefwefwef");

			response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
		}
	}
}
