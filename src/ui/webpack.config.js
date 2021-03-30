const path = require("path")
const webpack = require("webpack")
const MiniCssExtractPlugin = require("mini-css-extract-plugin")
const TerserPlugin = require("terser-webpack-plugin")

const webpackDevServerPort = 5002
const proxyTarget = "http://localhost:5001"

module.exports = (env, options) => {
  var mode = options.mode
  var isDevelopment = mode === "development"
  return {
    devServer: {
      compress: true,
      contentBase: path.join(__dirname, "dist"),
      proxy: {
        "*": {
          target: proxyTarget
        }
      },
      port: webpackDevServerPort
    },
    resolve: {
      extensions: [".less", ".ts", ".js", ".css", ".jsx"]
    },
    devtool: isDevelopment ? "inline-source-map" : false,
    entry: {
      app: ["./src/main.ts"]
    },
    output: {
      filename: "site.js",
      publicPath: "./dist/"
    },
    module: {
      rules: [
        {
          test: /\.less$/,
          use: [MiniCssExtractPlugin.loader, "css-loader", "less-loader"]
        },
        {
          test: /\.ts$/,
          loader: "ts-loader",
          exclude: [/node_modules/, "/wwwroot/"]
        }
      ]
    },
    plugins: [
      new MiniCssExtractPlugin({
        filename: "site.css"
      })
    ],
    optimization: {
      minimizer: [
        new TerserPlugin({
          terserOptions: {
            compress: {
              drop_console: true
            }
          }
        })
      ]
    }
  }

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
  return config
}
