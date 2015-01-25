﻿namespace Dashen.Components
{
	public class TextModel : Model
	{
		public string Text { get; set; }
	}

	public class TextComponent : Component<TextModel>
	{
		public override string GetJsx()
		{
			return @"
			var TextComponent = React.createClass({
			  render: function() {
			    return (
			      <p>{this.props.Text}</p>
			    );
			  }
			});
		";
		}
	}

}