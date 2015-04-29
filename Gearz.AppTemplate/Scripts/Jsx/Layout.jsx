import React from "react";
import HeaderLink from "./HeaderLink";

var _LoginPartial = React.createClass({
    render: function() {
        return (<span>_LoginPartial</span>);
    }
});

var Layout = React.createClass({
    render: function() {
        var viewData = this.props.viewData,
            appMeta = this.props.appMeta,
            areas = appMeta.areas;
        return (
            <div>
                <div className="navbar navbar-inverse navbar-fixed-top">
                    <div className="container">
                        <div className="navbar-header">
                            <a href={areas.root.home.index.url} className="navbar-brand">{this.props.viewData.name}</a>
                        </div>
                        <div className="navbar-collapse collapse">
                            <ul className="nav navbar-nav">
                                <li><HeaderLink data={areas.root.home.index} viewData={viewData} onAppData={this.props.onAppData} /></li>
                                <li><HeaderLink data={areas.root.home.about} viewData={viewData} onAppData={this.props.onAppData} /></li>
                                <li><HeaderLink data={areas.root.home.contact} viewData={viewData} onAppData={this.props.onAppData} /></li>
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

export default Layout;