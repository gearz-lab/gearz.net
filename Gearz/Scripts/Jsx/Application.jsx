var Application = React.createClass({
    handleAppData: function(data) {
        // alert(JSON.stringify(data));
        // alert(JSON.stringify(window.stores));
        // alert(Render);
        for (var k in data)
            window.stores.App[k] = data[k];
        Render();
    },
    render: function() {
        var _this = this;

        var location = this.props.App.location;

        var layout = function(children) {
            return (
                    <Layout areas={_this.props.Meta.areas} app={_this.props.App} onAppData={_this.handleAppData}>
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
