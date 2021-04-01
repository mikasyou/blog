import MiniCssExtractPlugin from "mini-css-extract-plugin"
import * as path from "path"
import TerserPlugin from "terser-webpack-plugin"
import * as webpack from "webpack"
import * as webpackDevServer from "webpack-dev-server"

interface Configuration extends webpack.Configuration {
  devServer?: webpackDevServer.Configuration
}

export default (env, options): Configuration => {
  const mode = options.mode
  console.log(mode)
  var isDevelopment = mode === "development"
  return {
    entry: {
      app: ["./src/main.ts"]
    },
    output: {
      path: path.resolve(__dirname, "dist"),
      filename: "site.js"
    },
    devServer: {
      contentBase: __dirname + "/",
      host: "0.0.0.0",
      port: 5002,
      proxy: [
        {
          context: ["**", "!**/*.css", "!**/*.js", "/api/**"],
          target: "https://localhost:5001",
          secure: false
        }
      ]
    },
    resolve: {
      extensions: [".less", ".ts", ".js", ".css", ".jsx"]
    },
    devtool: isDevelopment ? "source-map" : false,
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
}
