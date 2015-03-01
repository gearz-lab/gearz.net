var HeaderLink = React.createClass({
    handleClick: function(e) {
        e.preventDefault();
        var appData = {};

        // getting route values
        var route = window.gzRouter.getRouteFromUri(this.props.data.url);

        // determining whether an ajax request is needed or not
        appData.isLoading = route.isServer; // enables the activity indicator when a request is needed

        // updating based on data that is already available in client-side
        if (route.isClient) {
            for (var k in route.data)
                appData[k] = route.data[k];
        }

        // TODO: location must be route data, client data, or server data (not component data i.e. not in props)
        appData.location = this.props.data.location;

        // ajax request to update data based on server-side data
        if (route.isServer) {
        }

        this.props.onAppData(appData);
    },
    render: function() {
        var classAttr = this.props.appData.location == this.props.data.location ? "selected" : "";
        return (<a href={this.props.data.url}
            onClick={this.handleClick}
            className={classAttr}
            >{this.props.data.title}</a>);
    }
});
