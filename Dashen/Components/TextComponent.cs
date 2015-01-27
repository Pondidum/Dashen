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
			return @"{
			  render: function() {
			    return (
			      <p>Test: {this.props.model.Text} :End</p>
			    );
			  }
			}";
		}
	}

}
