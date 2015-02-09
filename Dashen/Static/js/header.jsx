var Header = React.createClass({

  loadFromServer: function() {
    $.ajax({
      url: this.props.url,
      dataType: 'json',
      success: function(data) {
        this.setState({model: data});
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });
  },

  getInitialState: function(){
    return {model: []};
  },

  componentDidMount: function() {
    this.loadFromServer();
  },

  render: function() {
    return (
      <div id="header" className="row clearfix">
        <div className="large-8 columns">
          <h1>{this.state.model.AppName}</h1>
        </div>
        <div id="version" className="large-4 columns">
          <p className="text-muted text-right">Version {this.state.model.AppVersion}</p>
        </div>
      </div>
    );
  }
});
