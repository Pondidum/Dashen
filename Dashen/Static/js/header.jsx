var Header = React.createClass({

	render: function() {
		return (
			<div id="header" className="row clearfix">
				<div className="large-8 columns">
					<h1>###APPTITLE###</h1>
				</div>
				<div id="version" className="large-4 columns">
					<p className="text-muted text-right">Version ###APPVERSION###</p>
				</div>
			</div>
		);
	}
});
