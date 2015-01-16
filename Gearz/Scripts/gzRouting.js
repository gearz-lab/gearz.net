// Gearz Routing    v0.1.0
//
//  This is responsible for routing, that is,
//  extracting information from an URI so that
//  an external agent can determine what to
//  render next.
//
//  This will not:
//  - change URI for single-page apps
//  - request server data in any way

(function () {
    var appVer = window.stores.appState.Versions.app,
        appMeta = window.stores.appMeta[appVer],
        routes = appMeta.routes;

    var router = {
        getRouteFromURI: function(uri) {
            for (var itR = 0; itR < routes.length; itR++) {
                var route = routes[itR];
                // trying to match the route information with the given URI

                // TODO: match the URI with the route
                if (route match URI)
                {
                    var r = {};
                    // copy route data to the resulting object
                    r.xpto = route.data;

                    return r;
                }
            }

            return null;
        }
    };

    return router;
})();