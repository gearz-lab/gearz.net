var NotFound = React.createClass({
    render: function() {
        var layout = this.props.layout || function(children) {
            <div>
                {children}
            </div>
        };
        return layout(
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
