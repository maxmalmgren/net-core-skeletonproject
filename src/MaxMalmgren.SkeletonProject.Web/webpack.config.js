const webpack = require('webpack');
const ExtractTextPlugin = require("extract-text-webpack-plugin");

const prod = process.argv.indexOf('-p') !== -1;

var bundleName = prod ? 'bundle.[hash:6].js' : 'bundle.js';
var stylesName = prod ? 'styles.[hash:6].css' : 'styles.css'

module.exports = {
    context: __dirname + '/wwwroot',
    entry: '../content/app.js',
    output: {
        filename: 'static/gen/' + bundleName,
        path: __dirname + '/wwwroot'
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                exclude: /node_modules/,
                loader: 'babel-loader',
            },
            {
                test: /\.vue$/,
                loader: "vue-loader",
                options: {
                    loaders: {
                        scss: 'vue-style-loader!css-loader!sass-loader', // <style lang="scss">
                        sass: 'vue-style-loader!css-loader!sass-loader?indentedSyntax' // <style lang="sass">
                    }
                }
            },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    fallback: "style-loader",
                    use: "css-loader"
                })
            },
            { test: /\.eot(\?v=\d+\.\d+\.\d+)?$/, loader: "file-loader?name=/static/gen/[name].[ext]" },
            { test: /\.(woff|woff2)$/, loader: "url-loader?prefix=font/&limit=5000&name=/static/gen/[name].[ext]" },
            { test: /\.ttf(\?v=\d+\.\d+\.\d+)?$/, loader: "url-loader?limit=10000&mimetype=application/octet-stream&name=/static/gen/[name].[ext]" },
            { test: /\.svg(\?v=\d+\.\d+\.\d+)?$/, loader: "url-loader?limit=10000&mimetype=image/svg+xml&name=/static/gen/[name].[ext]" }
        ]
    },
    plugins: [
        //new webpack.optimize.OccurrenceOrderPlugin(),
        //new webpack.DefinePlugin({
        //    'process.env': {
        //        'NODE_ENV': JSON.stringify('production')
        //    }
        //}),
        //new webpack.optimize.UglifyJsPlugin({
        new ExtractTextPlugin('static/gen/' + stylesName),
        //}),
        new webpack.ProvidePlugin({
            jQuery: 'jquery',
            $: 'jquery',
            jquery: 'jquery'
        })
    ],
    devtool: prod ? undefined : "inline-source-map"
}