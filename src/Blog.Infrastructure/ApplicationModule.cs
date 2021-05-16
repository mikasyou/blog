using Blog.Application.Articles;
using Blog.Domain.Articles;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.Queries;
using Blog.Infrastructure.Records;
using Blog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure {
    public class BlogDBOptions {
        public string ConnectionString { get; set; }
    }


    public class BlogDbContext : DbContext {
        public DbSet<ArticleRecord> Articles { get; set; }
        public DbSet<TagRecord> Tags { get; set; }

        public DbSet<CommentRecord> Comments { get; set; }
        public DbSet<ArticleAccessLogRecord> ArticleAccessLogs { get; set; }
        public DbSet<AuditRecord> Audits { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder
               .Entity<ArticleRecord>()
               .HasMany(p => p.Tags)
               .WithMany(p => p.Articles)
               .UsingEntity(j => j.ToTable("article_tags"));
        }
    }


    public static class ApplicationModule {
        public static void AddBlogServices(this IServiceCollection services) {
            services.AddScoped<ArticleService>();
            services.AddScoped<IArticleQueries, ArticleQueries>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
        }

        public static void AddPostgresContext(this IServiceCollection services, BlogDBOptions options) {
            services.AddDbContext<BlogDbContext>(builder => {
                builder.UseNpgsql(options.ConnectionString)
                       .UseSnakeCaseNamingConvention();
            });
        }
    }
}