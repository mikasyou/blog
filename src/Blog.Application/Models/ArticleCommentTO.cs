using System;

namespace Blog.Application.Models {
    public record ArticleCommentTO(
        string Id,
        string Avatar,
        string Name,
        string Content,
        string WebSite,
        DateTime CreateDate,
        string ReplyName
    );
}