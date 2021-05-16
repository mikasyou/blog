using System;
using System.Collections.Generic;
using Blog.Domain.Shared.Articles;

namespace Blog.Application.Articles.Models {
    public class ArticleData {
        public int Id { get; init; }
        public string Code { get; init; }
        public string Title { get; init; }
        public String Content { get; init; }
        public string Summary { get; init; }
        public string SubTitle { get; init; }
        public IEnumerable<ArticleTag> Tags { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime UpdateDate { get; init; }
        public int ReadCounts { get; init; }
        public List<ArticleComment> Comments { get; init; }

    }
}
