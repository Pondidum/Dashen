using Dashen.Controls;
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

			_builder = new DescriptorBuilder(engine, new NameTransformer());
		}

		private void HasTemplate(string path)
		{
			_viewFolder.HasView(path).Returns(true);
		}

		[Fact]
		public void When_loading_a_normal_view_and_there_is_no_application_view()
		{
			HasTemplate("Dashen\\Endpoints\\Index\\Index.spark");

			var descriptor = _builder.Build(typeof(IndexViewModel));

			descriptor.Templates.ShouldBe(new[] { "Dashen\\Endpoints\\Index\\Index.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_normal_view_and_there_is_an_application_view()
		{
			HasTemplate("Dashen\\Endpoints\\Index\\Index.spark");
			HasTemplate("Dashen\\Views\\Application.spark");

			var descriptor = _builder.Build(typeof(IndexViewModel));

			descriptor.Templates.ShouldBe(new[] { "Dashen\\Endpoints\\Index\\Index.spark", "Dashen\\Views\\Application.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_a_shared_view_and_there_is_no_application_view()
		{
			HasTemplate("Dashen\\Endpoints\\Index\\Index.spark");
			HasTemplate("Dashen\\Views\\TextWidget.spark");

			var descriptor = _builder.Build(typeof(TextWidgetModel));

			descriptor.Templates.ShouldBe(new[] { "Dashen\\Views\\TextWidget.spark" }, ignoreOrder: true);
		}

		[Fact]
		public void When_loading_a_shared_view_and_there_is_an_application_view()
		{
			HasTemplate("Dashen\\Endpoints\\Index\\Index.spark");
			HasTemplate("Dashen\\Views\\Application.spark");
			HasTemplate("Dashen\\Views\\TextWidget.spark");

			var descriptor = _builder.Build(typeof(TextWidgetModel));

			descriptor.Templates.ShouldBe(new[] { "Dashen\\Views\\TextWidget.spark" }, ignoreOrder: true);
		}
	}
}
