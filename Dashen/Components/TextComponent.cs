namespace Dashen.Components
{
	public class TextModel : Model
	{
		public string Text { get; set; }
	}

	public class TextComponent : Component<TextModel>
	{
		public override string GetJsx()
		{
			return @"React.createClass({
			  render: function() {
			    return (
			      <div className='text-center'>{this.props.model.Text} </div>
			    );
			  }
			});";
		}
	}

}
