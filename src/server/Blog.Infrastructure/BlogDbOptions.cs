using System;
using Blog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Blog.Domain.Shared.Article;
using Blog.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;


namespace Blog.Infrastructure {
    public class BlogDbOptions {
        public string ConnectionString { get; set; }
    }

    public class BlogDbContext : DbContext {
        public DbSet<ArticlePO> Articles { get; set; }
        public DbSet<ArticleTag> ArticleTags { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ArticleTag>()
                        .HasKey(it => it.Name);
        }
    }


    public static class BlogDatabaseConfiguration {
        public static void AddPostgresContext(this IServiceCollection services, BlogDbOptions options) {
            services.AddDbContext<BlogDbContext>(builder => {
                builder.UseNpgsql(options.ConnectionString)
                       .UseSnakeCaseNamingConvention();
            });
        }
    }
}