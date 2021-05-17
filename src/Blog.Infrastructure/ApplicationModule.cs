using Blog.Application.Articles;
using Blog.Domain.Articles;
using Blog.Infrastructure.Queries;
using Blog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure {
    public class BlogDBOptions {
#pragma warning disable CS8618
        public string ConnectionString { get; set; }
#pragma warning restore CS8618
    }


    public static class ApplicationModule {
        public static void AddBlogServices(this IServiceCollection services) {
            services.AddScoped<ArticleService>();
            services.AddScoped<IArticleQueries, ArticleQueries>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ArticleAccessService>();
        }

        public static void AddPostgresContext(this IServiceCollection services, BlogDBOptions options) {
            services.AddDbContext<BlogDbContext>(builder => {
                    builder.UseNpgsql(options.ConnectionString)
                           .UseSnakeCaseNamingConvention();
                }
            );
        }
    }
}