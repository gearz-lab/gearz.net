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
