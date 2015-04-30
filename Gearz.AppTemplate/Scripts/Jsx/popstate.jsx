import window from "window.jsx";
import update from "app.ui.update.jsx";

window.addEventListener('popstate', function(event) {
    if (event.state) {
        window.stores.viewData = event.state.viewData;
        var Component = window[event.state.pageComponent];
        if (Component) {
            React.render(
                    <Component />,
                    document.getElementById("output")
                );
            return;
        }
    }
    update();
});
