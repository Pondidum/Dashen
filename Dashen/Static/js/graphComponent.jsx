var GraphComponent = React.createClass({
  drawGraph: function() {
    var graphID = 'Graph' + this.props.model.ID;

    var model = this.props.model;

    if (model.Points == null) {
      return;
    }

    $.plot(
      document.getElementById(graphID),
      [model.Points],
      {
        xaxis: {
          tickLength: 0,
          ticks: model.XTicks
        },
        yaxis: {
          tickLength: 0,
          ticks: model.YTicks
        },
        colors: ["#248F24"],
        grid: {
          borderWidth: 0
        },
        series: {
          lines: {
            fill: true,
            fillColor: {colors: ["#ffffff", "#ffffff"]}
          },
        }
      }
    );

  },
  componentDidMount: function() {
    this.drawGraph();
  },
  componentDidUpdate: function() {
    this.drawGraph();
  },
  render: function() {
    var graphID = 'Graph' + this.props.model.ID;
    return (
      <div id={graphID} style={{width: '100%', height: '120px'}}></div>
    );
  }
});
