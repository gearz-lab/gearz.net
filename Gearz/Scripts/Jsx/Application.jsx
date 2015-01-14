var Application = React.createClass({
    handleAppData: function(data) {
        for (var k in data)
            window.stores.App[k] = data[k];
        Render();
    },
    getInitialState: function() {
        var _this = this;
        return {
                layout: React.createClass({
                    render: function() {
                        return (
                                <Layout areas={_this.props.Meta.areas} app={_this.props.App} onAppData={_this.handleAppData}>
                                    {this.props.children}
                                </Layout>
                            );
                    }
                })
            };
    },
    render: function() {
        var location = this.props.App.location;
        return (
            location == "Home" ?    <HomePage layout={this.state.layout} /> :
            location == "Contact" ? <ContactPage layout={this.state.layout} data={this.props.Data} /> :
                                    <NotFound layout={this.state.layout} />
		);
    }
});
