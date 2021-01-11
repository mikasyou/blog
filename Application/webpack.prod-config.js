const path = require("path");
var MiniCssExtractPlugin = require("mini-css-extract-plugin");


function getEntryPath(path) {
  return "./dev-src/" + path;
}

module.exports = [
  {
    resolve: {
      extensions: [".less", ".ts",".js",".css"]
    },
    entry: {
      "app": [
        getEntryPath("app/app.ts"),
        getEntryPath("app/index.ts")
      ]
    },
    output: {
      path: path.resolve(__dirname, "wwwroot/app")
    },
    module: {
      rules: [
        {
          test: /\.less$/,
          use: [
            MiniCssExtractPlugin.loader,
            "css-loader",
            "less-loader"
          ]
        },
        {
          test: /\.ts$/,
          use: 'ts-loader',
          exclude: [/node_modules/, "/wwwroot/"]
        }
      ]
    },

    plugins: [
      new MiniCssExtractPlugin({
        filename: "[name].css",
      })
    ]
  }
];
