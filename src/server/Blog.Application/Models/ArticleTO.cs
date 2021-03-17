using System;
using System.Collections.Generic;
using Blog.Domain.AggregatesModel.Aritcle;
using Blog.Domain.Core;

namespace Blog.Application.Models {
    public record ArticleTO(
        string Id,
        string Title,
        string SubTitle,
        IEnumerable<ArticleTag> Tags,
        string Content,
        DateTime CreateDate,
        DateTime UpdateDate,
        int ReadCounts,
        int CommentCounts,
        PageCollection<ArticleCommentTO> Comments,
        Dictionary<string, IEnumerable<ArticleCommentTO>> ReplyComments
    );
}