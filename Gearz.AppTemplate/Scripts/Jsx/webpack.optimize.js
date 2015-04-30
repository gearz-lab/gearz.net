var path = require('path');
var webpack = require('webpack');

module.exports = {
  //context: path.join(__dirname, 'Content'),
  entry: {
    //server: './entry-server.js',
    client: './entry-client.js'
  },
  output: {
    path: path.join(__dirname, 'build'),
    filename: '[name].bundle.min.js'
  },
  module: {
    loaders: [
      // Transform all javascript files
      { test: /\.jsx?$/, exclude: /node_modules/, loader: "babel-loader"},
      { test: /\.css$/, exclude: /node_modules/, loader: "style!css" },
      { test: require.resolve("./Application.jsx"), loader: "expose?Application!babel-loader" },
      { test: require.resolve("react"), loader: "expose?React" }
    ]
  },
  resolve: {
    // Allow require('./blah') to require blah.jsx
    extensions: ['', '.js', '.jsx']
  },
  externals: {
    // Use external version of React (from CDN for client-side, or
    // bundled with ReactJS.NET for server-side)
    react: 'React'
  },
  plugins: [
    new webpack.optimize.UglifyJsPlugin({minimize: true})
  ]
};