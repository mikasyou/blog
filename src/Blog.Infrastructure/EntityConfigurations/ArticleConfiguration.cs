using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityConfigurations {
    /// <summary>
    /// 文章持久化模型
    /// </summary>
    [Table("article")]
    public class ArticleConfiguration : IEntityTypeConfiguration<Article> {
        public void Configure(EntityTypeBuilder<Article> builder) {
            builder.ToTable("articles");
            builder.HasKey(o => o.Id);
            builder.Ignore(it => it.DomainEvents);

            builder.Property(it => it.Code).HasColumnType("varchar(256)").IsRequired();
            builder.Property(it => it.Title).HasColumnType("varchar(512)").IsRequired();
            builder.Property(it => it.SubTitle).HasColumnType("varchar(512)").IsRequired();
            builder.Property(it => it.Summary).HasColumnType("varchar(1024)").IsRequired();
            builder.Property(it => it.Content).HasColumnType("text").IsRequired();

            var articleId = nameof(Article) + nameof(Article.Id);

            builder.HasMany<ArticleComment>(it => it.Comments)
                   .WithOne()
                   .HasForeignKey(articleId)
                   .IsRequired();


            builder.HasMany<Tag>(it => it.Tags).WithMany(nameof(Article));
        }
    }
}