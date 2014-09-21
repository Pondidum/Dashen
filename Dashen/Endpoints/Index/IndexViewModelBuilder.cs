namespace Dashen.Endpoints.Index
{
	public class IndexViewModelBuilder
	{
		internal IndexDisplayViewModel FromWidget(Widget definition)
		{
			var model = new IndexDisplayViewModel
			{
				Heading = definition.Heading,
				ID = definition.ID,
				CreateWidgetUrl = "stats/createWidget/" + definition.ID,
				Columns = definition.Width,
			};

			return model;
		} 
	}
}
