using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Blog.Application.Articles;
using Blog.Application.Articles.Models;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Collections;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.Records;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Queries {
    public class ArticleQueries : IArticleQueries {
        private readonly DbSet<ArticleRecord> articles;
        private readonly DbSet<TagRecord> tags;
        private readonly Mapper mapper;

        public ArticleQueries(BlogDbContext database, AutoMapper.Mapper mapper) {
            tags = database.Tags;
            articles = database.Articles;
            this.mapper = mapper;
        }


        private Dictionary<string, TagRecord> ListTags() {
            return tags.Select(p => p).ToDictionary(it => it.ID, it => it);
        }

        public Page<ArticleIndexData> FindArticles(PagingLimit paging) {
            IQueryable<ArticleRecord> sql = articles;

            if (paging != null) {
                sql = sql.Skip(paging.Offset).Take(paging.Limit);
            }

            var total = articles.Count();
            var tagDict = ListTags();
            var items = sql.Select(it => new ArticleRecord {
                Id = it.Id,
                Code = it.Code,
                Title = it.Title,
                SubTitle = it.SubTitle,
                Summary = it.Summary,
                Tags = it.Tags.Select(tag => tagDict[tag.ID]).ToList(),
                AccessCounts = it.AccessCounts,
                CommentCounts = it.CommentCounts,
                CreateDate = it.CreateDate,
                UpdateDate = it.UpdateDate,
            }).ToList();
            var items2 = items.Select(it => this.mapper.Map<ArticleIndexData>(it));
            return new(items2, total);
        }

        public List<ArticleComment> FindComments(int artcileId) {
            throw new System.NotImplementedException();
        }
    }
}