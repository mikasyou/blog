using System;
using System.Collections.Generic;
using Blog.Domain.ArticleContext;

namespace Blog.Application.ArticleContext {
    public record ArticleTO(
        string Id,
        string Title,
        string SubTitle,
        IEnumerable<ArticleTag> Tags,
        string Summary,
        DateTime CreateDate,
        DateTime UpdateDate,
        int ReadCounts,
        int CommentCounts
    ) {
        public static ArticleTO From(Article it) {
            // TODO: content to summary
            return new(it.Id, it.Title, it.SubTitle, it.Tags, "", it.CreateDate, it.UpdateDate,
                       it.ReadCounts, it.CommentCounts);
        }
    }
}