using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Blog.Domain.Articles;

namespace Blog.Infrastructure.EntityConfigurations {
    [Table("article_access_log")]
    public class ArticleAccessLogConfiguration : IEntityTypeConfiguration<ArticleAccessLog> {
        public void Configure(EntityTypeBuilder<ArticleAccessLog> configuraion) {
            configuraion.HasKey(o => o.Id);
            configuraion.Property<int>("ArticleId")
                        .IsRequired();
        }
    }

    //[Key]
    //    [Column(TypeName = "serial")]
    //    public int Id { get; init; }

    //    public int ArticleId { get; init; }
    //    public DateTime CreateData { get; init; }

    //    [NotNull]
    //    [Column(TypeName = "varchar(128)")]
    //    public string? Ip { get; init; }
    //}
}