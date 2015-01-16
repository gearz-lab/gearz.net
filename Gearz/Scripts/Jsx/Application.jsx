var Application = React.createClass({
    handleAppData: function(data) {
        for (var k in data)
            window.stores.appData[k] = data[k];
        Render();
    },
    getInitialState: function() {
        var _this = this,
            appVer = this.props.appState.Versions.app,
            modVer = this.props.appState.Versions.module,
            appMeta = this.props.appMeta[appVer],
            appData = this.props.appData;
        return {
                layout: React.createClass({
                    render: function() {
                        return (
                                <Layout appMeta={appMeta} appData={appData} onAppData={_this.handleAppData}>
                                    {this.props.children}
                                </Layout>
                            );
                    }
                })
            };
    },
    render: function() {
        var location = this.props.appData.location;
        return (
            location == "Home" ?    <HomePage layout={this.state.layout} /> :
            location == "Contact" ? <ContactPage layout={this.state.layout} data={this.props.appData.pageData} /> :
                                    <NotFound layout={this.state.layout} />
		);
    }
});
