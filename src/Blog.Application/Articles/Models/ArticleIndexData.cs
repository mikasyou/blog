using System;
using System.Collections.Generic;
using Blog.Domain.Shared.Articles;

namespace Blog.Application.Articles.Models {
#pragma warning disable CS8618
    public class ArticleIndexData {
        public int Id { get; init; }
        public string Code { get; init; }
        public string Title { get; init; }
        public string Summary { get; init; }
        public string SubTitle { get; init; }
        public IEnumerable<ArticleTag> Tags { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime UpdateDate { get; init; }
        public int AccessCounts { get; init; }
        public int CommentCounts { get; init; }
    }
#pragma warning restore CS8618
}