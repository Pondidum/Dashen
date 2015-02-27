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

    var components = this.state.components;
    var last = components[components.length - 1];

    var wrapped = components.map(function(comp) {

      var isLast = comp == last;

      return (
        <Wrapper component={comp.Type} url={comp.Path} interval={5000} isLast={isLast} />
      );

    });

    return (
      <div id="container" className="row">
        <Header url="models/name/header" />
        <div className='row fullwidth'>
          {wrapped}
        </div>
        <Footer url="models/name/footer" />
      </div>
    );
  }
});
