using Blog.Application.Models;
using Blog.Domain.Core;

namespace Blog.Application.Queries {
    public interface IArticleQueries {
        public PageCollection<ArticleSummaryTO> ListArticles(int? offset, int? limit);
    }
}
