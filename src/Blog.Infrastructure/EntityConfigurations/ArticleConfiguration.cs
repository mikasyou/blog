using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Blog.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityConfigurations {
    /// <summary>
    /// 文章持久化模型
    /// </summary>
    [Table("article")]
    public class ArticleConfiguration : IEntityTypeConfiguration<Article> {
        public void Configure(EntityTypeBuilder<Article> builder) {
            builder.ToTable("articles", BlogDatabaseContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);
            builder.Ignore(it => it.DomainEvents);

            builder.Property(it => it.Code).HasColumnType("varchar(256)");
            builder.Property(it => it.Title).HasColumnType("varchar(512)");
            builder.Property(it => it.SubTitle).HasColumnType("varchar(512)");
            builder.Property(it => it.Summary).HasColumnType("varchar(1024)");
            builder.Property(it => it.Content).HasColumnType("text");

            builder.OwnsMany<ArticleComment>(it => it.Comments, p => {
                    p.HasKey(it => it.Id);
                    p.WithOwner().HasForeignKey("ArticleId");
                }
            );

            builder.OwnsMany<ArticleTag>(it => it.Tags, p => {
                    p.HasKey(it => it.Id);
                    p.WithOwner().HasForeignKey("ArticleId");
                }
            );
        }
    }
}