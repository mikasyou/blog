using System.Collections.Generic;
using Blog.Application.Articles.Models;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Collections;

namespace Blog.Application.Articles {
    public interface IArticleQueries {
        public Page<ArticleIndexData> FindArticles(PagingLimit paging);
        public List<ArticleComment> FindComments(int articleId);
    }
}