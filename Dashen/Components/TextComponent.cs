namespace Dashen.Components
{
	public class TextComponent : Component<TextModel>
	{
		public override string GetJsx()
		{
			return @"
var TextComponent = React.createClass({
  render: function() {
    return (
      <div className='text-center'>{this.props.model.Text} </div>
    );
  }
});";
		}
	}

}
