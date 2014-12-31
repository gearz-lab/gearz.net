var HeaderLink = React.createClass({
    handleClick: function(e) {
        e.preventDefault();
        var json = e.target.getAttribute("data-react") || null,
            data = json && JSON.parse(json);
        this.props.onAppData(data);
    },
    render: function() {
        var classAttr = this.props.app.location == this.props.data.location ? "selected" : "",
            staticData = JSON.stringify({
                    isLoading: true,
                    location: this.props.data.location
                });
        return (<a href={this.props.data.url}
            onClick={this.handleClick}
            data-react={staticData}
            className={classAttr}
            >{this.props.data.title}</a>);
    }
});

var _LoginPartial = React.createClass({
    render: function() {
        return (<span>_LoginPartial</span>);
    }
});

var Layout = React.createClass({
    render: function() {
        debugger;
        return (
            <div>
                <div className="navbar navbar-inverse navbar-fixed-top">
                    <div className="container">
                        <div className="navbar-header">
                            <a href={this.props.areas.root.home.index.url} className="navbar-brand">{this.props.app.name}</a>
                        </div>
                        <div className="navbar-collapse collapse">
                            <ul className="nav navbar-nav">
                                <li><HeaderLink data={this.props.areas.root.home.index} app={this.props.app} onAppData={this.props.onAppData} /></li>
                                <li><HeaderLink data={this.props.areas.root.home.about} app={this.props.app} onAppData={this.props.onAppData} /></li>
                                <li><HeaderLink data={this.props.areas.root.home.contact} app={this.props.app} onAppData={this.props.onAppData} /></li>
                            </ul>
                            <_LoginPartial />
                        </div>
                    </div>
                </div>
                <div className="container body-content">
                    {this.props.children}
                    <hr />
                    <footer>
                        <p>&copy; {this.props.app.year} - {this.props.app.company}</p>
                    </footer>
                </div>
            </div>
      );
    }
});
