using Dashen.Endpoints.Index;
using Dashen.Endpoints.Stats;
using Dashen.Infrastructure.Spark;
using NSubstitute;
using Shouldly;
using Spark;
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

			var engine = Substitute.For<ISparkViewEngine>();
			engine.ViewFolder.Returns(_viewFolder);

			_builder = new DescriptorBuilder(engine);
		}

		private void HasTemplate(string path)
		{
			_viewFolder.HasView(path).Returns(true);
		}

		[Fact]
		public void When_loading_a_normal_view_and_there_is_no_application_view()
		{
			HasTemplate("Endpoints\\Index\\Index.spark");

			var descriptor = _builder.Build(typeof(IndexViewModel));

			descriptor.Templates.ShouldBe(new[] { "Endpoints\\Index\\Index.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_normal_view_and_there_is_an_application_view()
		{
			HasTemplate("Endpoints\\Index\\Index.spark");
			HasTemplate("Views\\Application.spark");

			var descriptor = _builder.Build(typeof(IndexViewModel));

			descriptor.Templates.ShouldBe(new[] { "Endpoints\\Index\\Index.spark", "Views\\Application.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_a_shared_view_and_there_is_no_application_view()
		{
			HasTemplate("Endpoints\\Index\\Index.spark");
			HasTemplate("Views\\TextControl.spark");

			var descriptor = _builder.Build(typeof(TextControlViewModel));

			descriptor.Templates.ShouldBe(new[] { "Views\\TextControl.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_a_shared_view_and_there_is_an_application_view()
		{
			HasTemplate("Endpoints\\Index\\Index.spark");
			HasTemplate("Views\\Application.spark");
			HasTemplate("Views\\TextControl.spark");

			var descriptor = _builder.Build(typeof(TextControlViewModel));

			descriptor.Templates.ShouldBe(new[] { "Views\\TextControl.spark" }, ignoreOrder: true);
		}
	}
}
