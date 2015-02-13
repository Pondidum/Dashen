namespace Dashen.Components
{
	public class ListComponent : Component<ListModel>
	{
		public override string GetJsx()
		{
			return @"
var ListComponent = React.createClass({
  render: function() {

    var items = (this.props.model.Items || []).map(function(item) {
      return <li>{item}</li>;
    });

    return (
      <ul>{items}</ul>
    );
  }
});
";
		}
	}
}
