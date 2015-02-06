var Dashboard = React.createClass({

  loadFromServer: function() {
    $.ajax({
      url: this.props.url,
      dataType: 'json',
      success: function(data) {
        this.setState({components: data});
      }.bind(this),
      error: function(xhr, status, err) {
        console.error(this.props.url, status, err.toString());
      }.bind(this)
    });
  },

  getInitialState: function(){
    return {components: []};
  },

  componentDidMount: function() {
    this.loadFromServer();
    //setInterval(this.loadFromServer, this.props.interval);
  },

  render: function() {

    var wrapped = this.state.components.map(function(comp) {
      return (
        <Wrapper component={comp.Type} url={comp.Path} interval={5000} />
      );
    });

    return (
      <div className='row fullwidth'>
        {wrapped}
      </div>
    );
  }
});
