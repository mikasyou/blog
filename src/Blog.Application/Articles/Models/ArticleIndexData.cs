using System;
using System.Collections.Generic;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;

namespace Blog.Application.Articles.Models {
    public class ArticleIndexData {
        public int Id { get; init; }
        public string Code { get; init; } = default!;
        public string Title { get; init; } = default!;
        public string Summary { get; init; } = default!;
        public string SubTitle { get; init; } = default!;
        public IEnumerable<Tag> Tags { get; init; } = default!;
        public DateTime CreateDate { get; init; }
        public DateTime UpdateDate { get; init; }
        public int CommentCounts { get; init; }
    }
}