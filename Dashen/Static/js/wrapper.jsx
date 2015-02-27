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

  render: function () {
    var classes = "medium-" + this.state.model.Columns + " columns";

    if (this.props.isLast) {
      classes += " end";
    }

    return (
      <div className={classes}>
        <div className="panel widget">
          <h4> {this.state.model.Title}</h4>
          <hr />
          {React.createElement(window[this.props.component], {model: this.state.model})}
        </div>
      </div>
    );
  }
});

