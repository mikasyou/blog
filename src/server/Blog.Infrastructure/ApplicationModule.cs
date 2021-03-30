using System;
using Blog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Blog.Application.Queries;
using Blog.Application.Services;
using Blog.Domain.AggregatesModel.Article;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;


namespace Blog.Infrastructure {
    public class BlogDBOptions {
        public string ConnectionString { get; set; }
    }


    public class BlogDbContext : DbContext {
        public DbSet<ArticlePO> Articles { get; set; }
        public DbSet<TagPO> Tags { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder
               .Entity<ArticlePO>()
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