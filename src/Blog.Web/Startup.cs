using System.IO;
using System.Text.RegularExpressions;
using Blog.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace Blog.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var options = Configuration.GetSection("Blog").Get<BlogDBOptions>();
            services.AddBlogServices();
            services.AddPostgresContext(options);
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();

                //var options = new RewriteOptions().AddRedirect(".*\\.css", "http://localhost:5002")
                //                                  .AddRedirect(".*\\.js", "http://localhost:5002");

                //app.UseRewriter(options);
            }
            else {
                app.UseExceptionHandler("/Blog/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment()) {
                app.UseSpa(spa => {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:5002");
                });
            }
        }
    }
}