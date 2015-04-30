import "popstate.jsx";
import appPages from "app-pages.jsx";
import window from "window.jsx";
import gearzMixin from "gearz-react-mixin.jsx";
import React from "react";

var Link = React.createClass({
    mixins: [gearzMixin],
    processAjaxData: function (response, urlPath) {
        window.document.getElementById("content").innerHTML = response.html;
        window.document.title = response.pageTitle;
        window.history.pushState({"html": response.html, "pageTitle": response.pageTitle}, "", urlPath);
    },
    navigator: function (routeInfo) {
        return (e) => {
            var onNavigate = this.props.onNavigate;
            onNavigate && onNavigate(e);
            if (routeInfo.routeData.isClient) {
                //var currentInfo = this.props.router.getCurrentLocationInfo();
                var Component = appPages[routeInfo.routeData.pageComponent];
                var targetElement = window.document.getElementById(this.props.target);
                if (Component && targetElement) {
                    React.render(<Component />, targetElement);
                    window.history.pushState(
                        {
                            pageComponent: routeInfo.routeData.pageComponent,
                            viewData: {},
                            "pageTitle": "TITLE"
                        },
                        null,
                        routeInfo.uri);
                }
            }

            e.preventDefault();
        };
    },
    render: function () {
        var href = this.props.href,
            router = this.props.router,
            onNavigate = this.props.onNavigate; // triggered when navigating through this link

        var routeInfo = router.getInfo(href);

        return (
            <a href={routeInfo.uri} onClick={this.navigator(routeInfo)}>
                            {routeInfo.uri}
            </a>
        );
    }
});
