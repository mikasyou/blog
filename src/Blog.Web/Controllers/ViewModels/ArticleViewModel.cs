using System.Collections.Generic;
using Blog.Application.Articles.Models;
using Blog.Domain.Shared.Articles;

namespace Blog.Web.Controllers.ViewModels {
    public class ArticleViewModel {
        public ArticleData Article { get; init; }


        public List<ArticleComment> Comments { get; init; }
        public Dictionary<int, List<ArticleComment>> ChildrenComments { get; init; }
        public ArticleViewModel(ArticleData article, List<ArticleComment> comments, Dictionary<int, List<ArticleComment>> childrenComments) {
            this.Article = article;
            this.Comments = comments;
            this.ChildrenComments = childrenComments;
        }
    }
}
