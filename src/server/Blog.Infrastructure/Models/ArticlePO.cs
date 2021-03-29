using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Domain.AggregatesModel.Article;
using Blog.Domain.Shared.Article;

namespace Blog.Infrastructure.Models {
    /// <summary>
    /// 文章持久化模型
    /// </summary>
    public class ArticlePO {
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
        public int ReadCounts { get; init; }
        public int CommentCounts { get; init; }
        public DateTime CreateDate { get; init; } = DateTime.Now;
        public DateTime UpdateDate { get; init; } = DateTime.Now;
        public string Content { get; init; }
        public ICollection<TagPO> Tags { get; init; }
    }
}