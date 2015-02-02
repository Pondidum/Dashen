namespace Dashen.Components
{
	public class HeaderComponent : Component<HeaderModel>
	{
		public override string GetJsx()
		{
			return @"
React.createClass({
  render: function() {
    return (
      <div id='header' className='row clearfix'>
        <div className='large-8 columns'>
          <h1>{this.props.model.Name}</h1>
        </div>
        <div id='version' className='large-4 columns'>
          <p className='text-muted text-right'>Version {this.props.model.Version}</p>
        </div>
      </div>
    );
  }
});";
		}
	}
}