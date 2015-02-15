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
      return <li className=''>{item}</li>;
    });

    return (
      <ul className='no-bullet'>{items}</ul>
    );
  }
});
";
		}
	}
}
