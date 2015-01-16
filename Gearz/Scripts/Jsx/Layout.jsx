var _LoginPartial = React.createClass({
    render: function() {
        return (<span>_LoginPartial</span>);
    }
});

var Layout = React.createClass({
    render: function() {
        var appData = this.props.appData,
            appMeta = this.props.appMeta,
            areas = appMeta.areas;
        return (
            <div>
                <div className="navbar navbar-inverse navbar-fixed-top">
                    <div className="container">
                        <div className="navbar-header">
                            <a href={areas.root.home.index.url} className="navbar-brand">{this.props.appData.name}</a>
                        </div>
                        <div className="navbar-collapse collapse">
                            <ul className="nav navbar-nav">
                                <li><HeaderLink data={areas.root.home.index} appData={appData} onAppData={this.props.onAppData} /></li>
                                <li><HeaderLink data={areas.root.home.about} appData={appData} onAppData={this.props.onAppData} /></li>
                                <li><HeaderLink data={areas.root.home.contact} appData={appData} onAppData={this.props.onAppData} /></li>
                            </ul>
                            <_LoginPartial />
                        </div>
                    </div>
                </div>
                <div className="container body-content">
                    {this.props.children}
                    <hr />
                    <footer>
                        <p>&copy; {appMeta.app.year} - {appMeta.app.company}</p>
                    </footer>
                </div>
            </div>
      );
    }
});
