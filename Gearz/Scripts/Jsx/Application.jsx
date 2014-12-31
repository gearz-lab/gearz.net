

var Application = React.createClass({
    handleAppData: function(data) {
        mergeDeep(window.app, data);
    },
    render: function() {
        var _this = this;

        var location = this.props.app.location;
        return (
            location == "Home" ?    <HomePage layout={Layout} /> :
                                    <NotFound layout={Layout} />
		);
    }
});
