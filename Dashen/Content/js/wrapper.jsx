var Wrapper = React.createClass({

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
    setInterval(this.loadFromServer, this.props.interval);
  },
	render: function() {
		var model = { Text:"Shelley"};

    return <this.props.component model={this.state.model} />;
	}
});
