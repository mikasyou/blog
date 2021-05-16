using Blog.Infrastructure.Models;
using Blog.Infrastructure.Records;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure {
    public class BlogDbContext : DbContext {
#pragma warning disable CS8618
        public DbSet<ArticleRecord> Articles { get; init; }
        public DbSet<TagRecord> Tags { get; init; }

        public DbSet<CommentRecord> Comments { get; init; }
        public DbSet<ArticleAccessLogRecord> ArticleAccessLogs { get; init; }
        public DbSet<AuditRecord> Audits { get; init; }


        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) {
        }
#pragma warning restore CS8618


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<ArticleRecord>()
                        .HasMany(p => p.Tags)
                        .WithMany(p => p.Articles)
                        .UsingEntity(j => j.ToTable("article_tags"));

            modelBuilder.Entity<CommentRecord>()
                        .HasOne<ArticleRecord>()
                        .WithOne()
                        .HasForeignKey<CommentRecord>(p => p.ArticleId);

            modelBuilder.Entity<CommentRecord>()
                        .HasOne(p => p.Target);

            modelBuilder.Entity<ArticleAccessLogRecord>()
                        .HasOne<ArticleRecord>()
                        .WithOne()
                        .HasForeignKey<ArticleAccessLogRecord>(p => p.ArticleId);

            modelBuilder.Entity<CommentRecord>()
                        .HasOne<CommentRecord>()
                        .WithOne()
                        .HasForeignKey<CommentRecord>(p => p.RootId);
        }
    }
}