const path = require("path");
const webpack = require("webpack");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const autoprefixer = require("autoprefixer");
const TerserPlugin = require('terser-webpack-plugin');
function getEntryPath(path) {
  return "./dev-src/" + path;
}
module.exports = (env, options) => {
  var mode = options.mode;
  var isDevelopment = mode === "development";
  var config = {
    resolve: {
      extensions: [".less", ".ts", ".js", ".css", ".jsx"]
    },
    devtool: isDevelopment ? 'inline-source-map' : false,
    entry: {
      "app": [
        getEntryPath("app/main.ts")
      ]

    },
    output: {
      path: path.resolve(__dirname, "wwwroot/dist/")
    },
    module: {
      rules: [
        {
          test: /\.less$/,
          use: [
            MiniCssExtractPlugin.loader,
            'css-loader',
            {
              loader: 'postcss-loader',
              options: {
                plugins: () => {
                  let p = [autoprefixer({ browsers: ["> 1%"] })];
                  !isDevelopment && p.push(require("cssnano"));
                  return p;
                }
              }
            },
            "less-loader"
          ]
        },
        {
          test: /\.ts$/,
          loader: 'ts-loader',
          exclude: [/node_modules/, "/wwwroot/"]
        }
      ]
    },
    plugins: [
      new MiniCssExtractPlugin({
        filename: "[name].css",
      })
    ],
    optimization: {
      minimizer: [
        new TerserPlugin({
          sourceMap: true, // Must be set to true if using source-maps in production
          terserOptions: {
            compress: {
              drop_console: true,
            },
          },
        }),
      ]
    }
  };

  if (!isDevelopment) {


    // 生产环境压缩css
    //config.plugins.push(new OptimizeCSSAssetsPlugin({
    //  assetNameRegExp: /.+?.css/g, 
    //  cssProcessor: require('cssnano'),
    //  cssProcessorOptions: {
    //    safe: true,
    //    discardComments: { removeAll: true },
    //    autoprefixer: false
    //  },
    //  canPrint: true
    //}));
  }
  return config;
}
