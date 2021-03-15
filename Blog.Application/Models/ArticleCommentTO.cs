using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Application.ArticleContext;
using Blog.Domain.AggregatesModel.Aritcle;

namespace Blog.Application.Models {
    public record ArticleCommentTO(
        string                  Id,
        string                  Title,
        string                  SubTitle,
        IEnumerable<ArticleTag> Tags,
        string                  Summary,
        DateTime                CreateDate,
        DateTime                UpdateDate,
        int                     ReadCounts,
        int                     CommentCounts
    );
}