var Footer = React.createClass({

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
      <div class="row">
        <hr />
        <div class="large-12 columns">
          <p class="text-muted left">
            <a href="https://github.com/pondidum/Dashen">Github Project</a>
          </p>
          <p class="text-muted right">Written by Andy Dote, Version {this.state.model.DashenVersion}.</p>
        </div>
      </div>
    );
  }
});
