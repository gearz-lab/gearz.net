import { exec } from 'child-process-promise';
Promise.all(
    [
        exec("webpack --config webpack.optimize.js --devtool source-map"),
        exec("webpack --config webpack.dev.js --devtool source-map")
    ]
    );
