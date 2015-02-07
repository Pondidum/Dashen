var Dashboard = React.createClass({

  getInitialState: function(){
    return {components: []};
  },

  componentDidMount: function() {
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
