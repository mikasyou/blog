using System.Collections.Generic;
using System.Linq;
using Blog.Application.Models;
using Blog.Application.Queries;
using Blog.Domain.Core;
using Blog.Domain.Shared.Article;
using Blog.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Queries {
    public class ArticleQueries : IArticleQueries {
        private readonly DbSet<ArticlePO> _articles;
        private readonly DbSet<TagPO> _tags;


        public ArticleQueries(BlogDbContext database) {
            _tags = database.Tags;
            _articles = database.Articles;
        }


        private Dictionary<string, ArticleTag> ListTags() {
            return this._tags.Select(tag => new ArticleTag(tag.ID, tag.Value)).ToDictionary(it => it.ID, it => it);
        }

        public PageCollection<ArticleSummary> ListArticles(int? offset, int? limit) {
            IQueryable<ArticlePO> sql = _articles;
            if (offset != null && limit != null) {
                sql = sql.Skip(offset.Value).Take(limit.Value);
            }

            var total = _articles.Count();
            var tagDict = ListTags();
            return new PageCollection<ArticleSummary>(
                sql.Select(it => new ArticleSummary(
                        it.ID, it.Title, it.SubTitle,
                        it.Tags.Select(tag => tagDict[tag.ID]).ToList(),
                        it.Summary, it.CreateDate,
                        it.UpdateDate, it.ReadCounts, it.CommentCounts
                    ))
                   .ToList(),
                total
            );
        }
    }
}