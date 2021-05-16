import MiniCssExtractPlugin from "mini-css-extract-plugin"
import * as path from "path"
import TerserPlugin from "terser-webpack-plugin"
import * as webpack from "webpack"

// interface Configuration extends webpack.Configuration {
//   devServer?: webpackDevServer.Configuration
// }

export default (env: any, options: any): webpack.Configuration => {
  const mode = options.mode
  console.log(mode)
  const isDevelopment = mode === "development"
  return {
    entry: {
      app: ["./Assets/main.ts"]
    },
    output: {
      path: path.resolve(__dirname, "wwwroot/dist"),
      filename: "site.js"
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
