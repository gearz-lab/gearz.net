var Application = React.createClass({
    handleAppData: function(data) {
        //mergeDeep(window.app, data);
    },
    render: function() {
        var _this = this;

        var location = this.props.app.location;

        var layout = function(children) {
            return (
                    <Layout areas={_this.props.areas} app={_this.props.app}>
                        {children}
                    </Layout>
                );
        };

        return (
            location == "Home" ?    <HomePage layout={layout} /> :
                                    <NotFound layout={layout} />
		);
    }
});
