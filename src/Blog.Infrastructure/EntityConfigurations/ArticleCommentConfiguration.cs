using Blog.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityConfigurations {
    class ArticleCommentConfiguration : IEntityTypeConfiguration<ArticleComment> {
        public void Configure(EntityTypeBuilder<ArticleComment> builder) {
            builder.ToTable("article_comments");
            builder.HasKey(o => o.Id);

            builder.OwnsOne(it => it.TargetName, p => {
                    p.WithOwner().HasForeignKey(nameof(ArticleComment.TargetId));
                }
            );

            builder.HasOne<ArticleComment>()
                   .WithOne()
                   .HasForeignKey(nameof(ArticleComment.RootId))
                   .IsRequired(false);

            builder.HasOne<ArticleComment>()
                   .WithOne()
                   .HasForeignKey(nameof(ArticleComment.TargetId))
                   .IsRequired(false);
        }
    }
}