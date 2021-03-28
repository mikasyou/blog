using System.Linq;
using Blog.Application.Models;
using Blog.Application.Queries;
using Blog.Domain.Core;
using Blog.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Queries {
    public class ArticleQueries : IArticleQueries {
        private readonly DbSet<ArticlePO> _articles;

        public ArticleQueries(BlogDbContext database) {
            _articles = database.Articles;
        }

        public PageCollection<ArticleSummaryTO> ListArticles(int? offset, int? limit) {
            IQueryable<ArticlePO> sql = _articles;
            if (offset != null && limit != null) {
                sql = sql.Skip(offset.Value).Take(limit.Value);
            }

            var total = _articles.Count();
            return new PageCollection<ArticleSummaryTO>(
                sql.Select(it => new ArticleSummaryTO(
                        it.ID, it.Title, it.SubTitle, it.Tags, it.Summary, it.CreateDate,
                        it.UpdateDate, it.ReadCounts, it.CommentCounts
                    ))
                   .ToList(),
                total
            );
        }
    }
}