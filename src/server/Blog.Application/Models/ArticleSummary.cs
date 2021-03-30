using System;
using System.Collections.Generic;
using Blog.Domain.Shared.Article;

namespace Blog.Application.Models {
    public record ArticleSummary(
        int Id,
        string Code,
        string Title,
        string SubTitle,
        ICollection<ArticleTag> Tags,
        string Summary,
        DateTime CreateDate,
        DateTime UpdateDate,
        int ReadCounts,
        int CommentCounts
    );
}