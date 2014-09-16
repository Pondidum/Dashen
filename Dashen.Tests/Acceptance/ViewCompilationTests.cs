using System;
using System.IO;
using System.Text;
using Dashen.Endpoints.Index;
using Dashen.Endpoints.Stats;
using Dashen.Infrastructure.Spark;
using Shouldly;
using Xunit;

namespace Dashen.Tests.Acceptance
{
	public class ViewCompilationTests
	{
		private readonly SparkEngine _spark;

		public ViewCompilationTests()
		{
			var config = new DashenConfiguration();
			config.AddControlView<FakeControlViewModel>(CreateView());

			var app = new ApplicationModel(config);

			var configuredEngine = new SparkBuilder(config).Build();
			var descriptorBuilder = new DescriptorBuilder(configuredEngine);

			_spark = new SparkEngine(configuredEngine, descriptorBuilder, app);
		}

		[Fact]
		public void A_control_view_with_no_template_renders()
		{
			var model = new TextControlViewModel { Content = "Test Text" };

			var content = Render(model);
			content.ShouldContain("<div class=\"text-center\">Test Text</div>");
		}

		[Fact]
		public void A_view_with_template_renders()
		{
			var model = new IndexViewModel();

			var content = Render(model);

			content.ShouldNotBeEmpty();
		}

		[Fact]
		public void A_custom_view_can_be_added_and_compiled()
		{
			var model = new FakeControlViewModel();

			var content = Render(model);
			content.ShouldContain("<p>Testing omg!</p>");
		}


		private byte[] CreateView()
		{
			var view =
				@"<viewdata model='Dashen.Tests.Acceptance.FakeControlViewModel' />" + Environment.NewLine +
				@"<p>!{Model.Content}</p>";

			return Encoding.UTF8.GetBytes(view);
		}

		private string Render<T>(T model)
		{
			var view = _spark.CreateView(model);

			var sb = new StringBuilder();
			using (var sw = new StringWriter(sb))
			{
				view.RenderView(sw);
				sw.Flush();
			}

			return sb.ToString();
		}
	}

	public class FakeControlViewModel : ControlViewModel
	{
		public string Content { get; set; }

		public FakeControlViewModel()
		{
			Content = "Testing omg!";
		}
	}
}