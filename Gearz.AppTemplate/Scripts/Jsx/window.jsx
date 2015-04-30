// we expect the following globals in this file:
//  - window: represents the current window
//  - Window: the constructor of the global window object
var _this;
(function(){ _this = this; })();
var isInBrowser = _this instanceof Window && window === _this;
export default isInBrowser ? _this : null;