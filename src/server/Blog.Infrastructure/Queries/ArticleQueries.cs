using System.Linq;
using Blog.Application.Models;
using Blog.Application.Queries;
using Blog.Domain.Core;
using Blog.Infrastructure.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Blog.Infrastructure.Queries {
    public class ArticleQueries : IArticleQueries {
        private readonly IMongoCollection<ArticlePO> _articles;


        public ArticleQueries(IMongoCollection<ArticlePO> articles) {
            _articles = articles;

        }

        public PageCollection<ArticleSummaryTO> ListArticles(int? offset, int? limit) {
            var sql = _articles.Find(new BsonDocument());
            if (offset != null && limit != null) {
                sql = sql.Skip(offset).Limit(limit);
            }

            var total = _articles.CountDocuments(new BsonDocument());
            return new PageCollection<ArticleSummaryTO>(
                sql.ToList().Select(it => new ArticleSummaryTO(it.Id, it.Title, it.SubTitle, it.Tags, "", it.CreateDate, it.UpdateDate, it.ReadCounts, it.CommentCounts)),
                (int)total
            );
        }
    }
}
