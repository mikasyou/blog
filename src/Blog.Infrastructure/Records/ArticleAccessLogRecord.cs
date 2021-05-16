using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Domain.Articles;

namespace Blog.Infrastructure.Records {

    [Table("article_access_log")]
    public class ArticleAccessLogRecord {
        [Key]
        public int Id { get; init; }
        [Column("article_id")]
        public Article Article { get; init; }
        public DateTime CreateData { get; init; }
        [Column(TypeName = "varchar(128)")]
        public string Ip { get; init; }
    }
}
