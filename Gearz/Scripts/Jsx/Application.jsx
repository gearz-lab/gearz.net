var Application = React.createClass({
    handleViewData: function(data) {
        for (var k in data)
            window.stores.viewData[k] = data[k];
        Render();
    },
    getInitialState: function() {
        var _this = this,
            appVer = this.props.appState.Versions.app,
            modVer = this.props.appState.Versions.module,
            appMeta = this.props.appMeta[appVer],
            viewData = this.props.viewData;
        return {
                layout: React.createClass({
                    render: function() {
                        return (
                                <Layout appMeta={appMeta} viewData={viewData} onAppData={_this.handleAppData}>
                                    {this.props.children}
                                </Layout>
                            );
                    }
                })
            };
    },
    render: function() {
        var location = this.props.viewData.location;
        return (
            location == "Home" ?    <HomePage layout={this.state.layout} /> :
            location == "Contact" ? <ContactPage layout={this.state.layout} data={this.props.viewData.pageData} /> :
                                    <NotFound layout={this.state.layout} />
		);
    }
});
