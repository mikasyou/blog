using Blog.Application.ArticleContext;
using Blog.Domain.Core;

namespace Blog.Application.Queries {
    public interface IArticleQueries {
        public PageCollection<ArticleTO> ListArticles(int? offset, int? limit);
    }
}
