'use strict';

const webpack = require('webpack');

// https://webpack.js.org/plugins/context-replacement-plugin/
module.exports = {
  plugins: [new webpack.ContextReplacementPlugin(/moment[/\\]locale$/, /bs/, /de/, /en/)]
};