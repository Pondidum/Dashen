using Dashen.Infrastructure.Spark;
using NSubstitute;
using Shouldly;
using Spark.FileSystem;
using Xunit;

namespace Dashen.Tests.Infrastructure.Spark
{
	public class DescriptorBuilderTests
	{
		private readonly IViewFolder _viewFolder;
		private readonly DescriptorBuilder _builder;

		public DescriptorBuilderTests()
		{
			_viewFolder = Substitute.For<IViewFolder>();
			_builder = new DescriptorBuilder(_viewFolder);
		}

		private void HasTemplate(string path)
		{
			_viewFolder.HasView(path).Returns(true);
		}

		[Fact]
		public void When_loading_a_normal_view_and_there_is_no_application_view()
		{
			HasTemplate("Index.spark");

			var descriptor = _builder.Build("Index.spark");

			descriptor.Templates.ShouldBe(new[] { "Index.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_normal_view_and_there_is_an_application_view()
		{
			HasTemplate("Index.spark");
			HasTemplate("Shared\\Application.spark");

			var descriptor = _builder.Build("Index.spark");

			descriptor.Templates.ShouldBe(new[] { "Index.spark", "Shared\\Application.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_a_shared_view_and_there_is_no_application_view()
		{
			HasTemplate("Index.spark");
			HasTemplate("Shared\\SomePartial.spark");

			var descriptor = _builder.Build("SomePartial.spark");

			descriptor.Templates.ShouldBe(new[] { "Shared\\SomePartial.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_a_shared_view_and_there_is_an_application_view()
		{
			HasTemplate("Index.spark");
			HasTemplate("Shared\\Application.spark");
			HasTemplate("Shared\\SomePartial.spark");

			var descriptor = _builder.Build("SomePartial.spark");

			descriptor.Templates.ShouldBe(new[] { "Shared\\SomePartial.spark" }, ignoreOrder: true);
		}
	}
}
