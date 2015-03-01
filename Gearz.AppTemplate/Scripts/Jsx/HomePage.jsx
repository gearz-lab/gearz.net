var HomePage = React.createClass({
    render: function() {
        var Layout = this.props.layout;
        var result = (
            <Layout>
                <div className="jumbotron">
                    <h1>ASP.NET with Gearz</h1>
                    <p className="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS and JavaScript.</p>
                    <p className="lead">Gearz is a free web framework for building even greater Web sites and Web applications using Facebook ReactJS, CSS and JavaScript.</p>
                    <p>
						<a style={{margin: "10px"}} href="https://github.com/gearz-lab/gearz.net" className="btn btn-primary btn-lg">Learn more about Gearz &raquo;</a>
						<a href="http://asp.net" className="btn btn-primary btn-sm">Learn more about ASP.NET &raquo;</a>
					</p>
                </div>

                <div className="row">
                    <div className="col-md-4">
                        <h2>Getting started</h2>
                        <p>
                            ASP.NET MVC gives you a powerful, patterns-based way to build dynamic websites that
                            enables a clean separation of concerns and gives you full control over markup
                            for enjoyable, agile development.
                        </p>
                        <p><a className="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301865">Learn more &raquo;</a></p>
                    </div>
                    <div className="col-md-4">
                        <h2>Get more libraries</h2>
                        <p>NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.</p>
                        <p><a className="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301866">Learn more &raquo;</a></p>
                    </div>
                    <div className="col-md-4">
                        <h2>Web Hosting</h2>
                        <p>You can easily find a web hosting company that offers the right mix of features and price for your applications.</p>
                        <p><a className="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301867">Learn more &raquo;</a></p>
                    </div>
                </div>
            </Layout>
		);
        return result;
    }
});
