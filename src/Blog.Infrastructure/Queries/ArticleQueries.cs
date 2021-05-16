using System.Collections.Generic;
using System.Linq;
using Blog.Application.Articles;
using Blog.Application.Articles.Models;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Collections;
using Blog.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Queries {
    public class ArticleQueries : IArticleQueries {
        private readonly DbSet<ArticleRecord> articles;
        private readonly DbSet<TagRecord> tags;


        public ArticleQueries(BlogDbContext database) {
            tags = database.Tags;
            articles = database.Articles;
        }


        private Dictionary<string, ArticleTag> ListTags() {
            return tags.Select(tag => new ArticleTag(tag.ID, tag.Value)).ToDictionary(it => it.ID, it => it);
        }

        public Page<ArticleIndexData> FindArticles(PagingLimit paging) {
            IQueryable<ArticleRecord> sql = articles;

            if (paging != null) {
                sql = sql.Skip(paging.Offset).Take(paging.Limit);
            }

            var total = articles.Count();
            var tagDict = ListTags();
            var items = sql.Select(it => new ArticleIndexData {
                Id = it.ID,
                Code = it.Code,
                Title = it.Title,
                SubTitle = it.SubTitle,
                Summary = it.Summary,
                Tags = it.Tags.Select(tag => tagDict[tag.ID]).ToList(),
                ReadCounts = it.AccessCounts,
                CommentCounts = it.CommentCounts,
                CreateDate = it.CreateDate,
                UpdateDate = it.UpdateDate,
            }).ToList();

            return new(items, total);
        }

        public List<ArticleComment> FindComments(int artcileId) {
            throw new System.NotImplementedException();
        }
    }
}