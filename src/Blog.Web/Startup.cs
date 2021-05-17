using System.Linq;
using AutoMapper;
using Blog.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Blog.Web {
    public static class Application {
        public static IMvcBuilder AddControllersAsServices(
            this IMvcBuilder builder,
            ServiceLifetime lifetime
        ) {
            var feature = new ControllerFeature();
            builder.PartManager.PopulateFeature(feature);

            foreach (var controller in feature.Controllers.Select(c => c.AsType())) {
                builder.Services.Add(
                    ServiceDescriptor.Describe(controller, controller, lifetime)
                );
            }

            builder.Services.Replace(ServiceDescriptor
               .Transient<IControllerActivator, ServiceBasedControllerActivator>()
            );

            return builder;
        }
    }

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
            services.AddAutoMapper(new[] {
                    typeof(ApplicationModule).Assembly
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Blosg/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                    endpoints.MapControllers();
                }
            );
        }
    }
}