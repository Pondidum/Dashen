namespace Dashen.Components
{
	public class HtmlComponent : Component<HtmlModel>
	{
		public override string GetJsx()
		{
			return @"
var HtmlComponent = React.createClass({
  render: function() {
    var rawMarkup = this.props.model.Html;
    return (
      <div className='text-center' dangerouslySetInnerHTML={{__html: rawMarkup}} />
    );
  }
});";
		}
	}
}
