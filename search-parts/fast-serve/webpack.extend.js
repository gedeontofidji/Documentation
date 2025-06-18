/*
 * User webpack settings file. You can add your own settings here.
 * Changes from this file will be merged into the base webpack configuration file.
 * This file will not be overwritten by the subsequent spfx-fast-serve calls.
 */

const webpack = require("webpack");
const { IgnorePlugin } = require('webpack');
const { ProvidePlugin } = require('webpack'); 

// you can add your project related webpack configuration here, it will be merged using webpack-merge module
// i.e. plugins: [new webpack.Plugin()]
const webpackConfig = {
    resolve: {
        alias: { 
          handlebars: 'handlebars/dist/handlebars.min.js',
          process: 'process/browser',
        },
        fallback: {
          "path": require.resolve("path-browserify"),
          "util": require.resolve("util/"),
          "url": require.resolve("url/"),
          "querystring": require.resolve("querystring-es3"),
          "fs": false
        }
    },
    module: {
        rules: [{
          test: /\.js$/,
          include: [
            /lit/, 
            /@lit/, 
            /lit-html/
          ],
          use: {
            loader: 'babel-loader',
            options: {
              plugins: [
                '@babel/plugin-transform-optional-chaining',
                '@babel/plugin-transform-nullish-coalescing-operator',
                '@babel/plugin-transform-logical-assignment-operators'
              ]
            }
          }
        }, 
        {
          test: /utils\.js$/,
          loader: "unlazy-loader",
          include: [
              /node_modules/,
          ]
        },
        {
            test: /index.js$/,
            loader: 'string-replace-loader',
            include: [
                /handlebars-helpers/,
            ],
            options: {
                search: '(logging|markdown): require.*?,',
                replace: '',
                flags: 'g'
            }
        }, 
        {
          // Skip HoverReactionsBar from spfx controls as it's not used and is bundles
          test: /index\.js$/,
          include: [/spfx-controls-react[/\\]lib[/\\]controls[/\\]HoverReactionsBar/],
          loader: 'ignore-loader',
        }
      ]
    },
    optimization: {
      splitChunks: {
        cacheGroups: {
          vendors: false
        }
      }
    },
    plugins: [
      new IgnorePlugin({
        resourceRegExp: /^\.\/locale$/, // Example: Ignore all locale files
        contextRegExp: /moment$/ // Example: Ignore locale files in moment package
      }),
      new ProvidePlugin({
        process: 'process/browser'
      }),
    ]
}

// for even more fin-grained control, you can apply custom webpack settings using below function
const transformConfig = function(initialWebpackConfig) {
    // transform the initial webpack config here, i.e.
    // initialWebpackConfig.plugins.push(new webpack.Plugin()); etc.

    return initialWebpackConfig;
}

module.exports = {
    webpackConfig,
    transformConfig
}