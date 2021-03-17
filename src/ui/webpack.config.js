const path = require("path");
const webpack = require("webpack");
module.exports = (env, options) => {
  var mode = options.mode;
  var isDevelopment = mode === "development";
  var config = {
    resolve: {
      extensions: [".less", ".ts", ".js", ".css", ".jsx"],
    },
    devtool: isDevelopment ? "inline-source-map" : false,
    entry: {
      app: ["src/app/main.ts"],
    },
    output: {
      path: path.resolve(__dirname, "wwwroot/dist/"),
    },
    module: {
      rules: [
        {
          test: /\.less$/,
          use: ["css-loader", "less-loader"],
        },
        {
          test: /\.ts$/,
          loader: "ts-loader",
          exclude: [/node_modules/, "/wwwroot/"],
        },
      ],
    },
    plugins: [
      new MiniCssExtractPlugin({
        filename: "[name].css",
      }),
    ],
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
};
