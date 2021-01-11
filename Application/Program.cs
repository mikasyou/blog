using AKiuLog;
using AKiuLog.Message;
using AyaBlog.DaoService;
using AyaBlog.Middle;
using Framework.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;

namespace AyaBlog
{
  public class Program
  {

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      // 为null自动判定为生产模式
      Console.WriteLine("命令行参数：" + string.Join(",", args));
      string mode = args.FirstOrDefault(m => m.IndexOf("mode=") == 0);
      string environmentName = SetEnvironment(mode);
      Console.WriteLine("运行环境:" + environmentName);

      bool isDevelopment = environmentName.Equals("development");
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{environmentName}.json", true, true)
        .Build();

      string webRootPath = config.GetValue<string>("WebRootPath");


      var host = WebHost.CreateDefaultBuilder(args)
        .UseEnvironment(environmentName)
        .UseConfiguration(config)
        .UseUrls(config["Urls"].Split(";"))
        // 注册日志队列
        .AddAKiuLogger(config["LogFilePath"])
        // 
        .ConfigureServices(services =>
        {

          services.Configure<CookiePolicyOptions>(options =>
          {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
          });

          // 添加过滤器
          services.AddMvc(options =>
          {
            options.Filters.Add(new AKiuLogRequestFilter());
            options.Filters.Add(new LogExceptionFilter());
          })
          .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

          services.AddDbManager(config.GetConnectionString("blog"))
                  .AddService<BlogDbService>();
          services.Configure<ForwardedHeadersOptions>(options =>
          {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
          });
        })
        .Configure((app) =>
        {
          if (isDevelopment)
          {
            app.UseDeveloperExceptionPage();
          }
          else
          {
            app.UseExceptionHandler("/blog/Error");
          }
          app.UseStaticFiles();
          app.UseMvc(routes =>
          {
            routes.MapRoute("default", "{controller=blog}/{action=index}");
          });
        })
        .UseWebRoot(webRootPath);



      return host;

    }


    public static void Main(string[] args)
    {

      CreateWebHostBuilder(args).Build().Run();
    }

    public static string SetEnvironment(string mode)
    {
      if (!string.IsNullOrEmpty(mode))
      {
        return mode.Split("=")[1];
      }
      string defaultName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
      return string.IsNullOrEmpty(defaultName) ? "production" : defaultName;
    }
  }
}