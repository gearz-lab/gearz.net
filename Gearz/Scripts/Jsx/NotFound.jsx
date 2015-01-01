var NotFound = React.createClass({
    render: function() {
        return this.props.layout(
			<div className="row">
				<div className="col-md-12">
			        <h1>Page not found</h1>
					<p>
						The requested page does not appear to exist.
					</p>
				</div>
			</div>
		);
    }
});
