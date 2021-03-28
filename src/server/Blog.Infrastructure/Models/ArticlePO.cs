using System;
using System.Collections.Generic;
using Blog.Domain.AggregatesModel.Article;
using Blog.Domain.Shared.Article;

namespace Blog.Infrastructure.Models {
    /// <summary>
    /// 文章持久化模型
    /// </summary>
    public class ArticlePO {
        public string ID { get; init; }
        public string Title { get; init; }
        public string SubTitle { get; init; }
        public ICollection<ArticleTag> Tags { get; init; }
        public ArticleState State { get; init; }
        public string Summary { get; init; }
        public int ReadCounts { get; init; }
        public int CommentCounts { get; init; }
        public DateTime CreateDate { get; init; } = DateTime.Now;
        public DateTime UpdateDate { get; init; } = DateTime.Now;
        public string Content { get; init; }
    }
}