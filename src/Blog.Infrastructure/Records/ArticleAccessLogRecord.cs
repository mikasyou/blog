using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Blog.Infrastructure.Records {
    [Table("article_access_log")]
    public class ArticleAccessLogRecord {
        [Key]
        [Column(TypeName = "serial")]
        public int Id { get; init; }

        public int ArticleId { get; init; }
        public DateTime CreateData { get; init; }

        [NotNull]
        [Column(TypeName = "varchar(128)")]
        public string? Ip { get; init; }
    }
}