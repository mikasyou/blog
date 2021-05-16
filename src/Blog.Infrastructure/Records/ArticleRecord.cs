#pragma warning disable CS8618
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Domain.Shared.Articles;
using Blog.Infrastructure.Models;

namespace Blog.Infrastructure.Records {

    /// <summary>
    /// 文章持久化模型
    /// </summary>
    [Table("article")]
    public class ArticleRecord {
        [Key]
        public int ID { get; init; }

        [Column(TypeName = "varchar(256)")]
        public string Code { get; init; }

        [Column(TypeName = "varchar(512)")]
        public string Title { get; init; }

        [Column(TypeName = "varchar(1024)")]
        public string SubTitle { get; init; }
        public ArticleState State { get; init; }
        public string Summary { get; init; }
        public DateTime CreateDate { get; init; } = DateTime.Now;
        public DateTime UpdateDate { get; init; } = DateTime.Now;
        public int AccessCounts { get; init; }
        public int CommentCounts { get; init; }
        public string Content { get; init; }
        public ICollection<TagRecord> Tags { get; init; }
    }
}