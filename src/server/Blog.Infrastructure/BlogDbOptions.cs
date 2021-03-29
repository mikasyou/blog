using System;
using Blog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using Blog.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;


namespace Blog.Infrastructure {
    public class BlogDbOptions {
        public string ConnectionString { get; set; }
    }

    public class
        BlogDbContext : DbContext {
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


    public static class BlogDatabaseConfiguration {
        public static void AddPostgresContext(this IServiceCollection services, BlogDbOptions options) {
            services.AddDbContext<BlogDbContext>(builder => {
                builder.UseNpgsql(options.ConnectionString)
                       .UseSnakeCaseNamingConvention();
            });
        }
    }
}