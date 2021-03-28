using System;
using System.Collections.Generic;
using Blog.Domain.Shared.Article;

namespace Blog.Application.Models {
    public record ArticleSummaryTO(
        string Id,
        string Title,
        string SubTitle,
        IEnumerable<ArticleTag> Tags,
        string Summary,
        DateTime CreateDate,
        DateTime UpdateDate,
        int ReadCounts,
        int CommentCounts
    );
}