using System;
using System.Linq;
using Blog.Domain.AggregatesModel.Article;
using Blog.Domain.Shared.Article;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure.Repositories {
    public class ArticleRepository : IArticleRepository {
        private readonly BlogDbContext _database;


        public ArticleRepository(BlogDbContext database) {
            _database = database;
        }

        public Article FindById(int articleId) {
            var po = _database.Articles.FirstOrDefaultAsync(it => it.ID == articleId).Result;
            if (po == null)
                throw new NullReferenceException($"未找到指定文章，id:{articleId}");

            return new Article(po.ID, po.Title, po.CommentCounts, po.ReadCounts, po.SubTitle,
                po.Tags.Select(it => new ArticleTag(it.ID, it.Value)).ToList(),
                po.CreateDate,
                po.CreateDate, po.Content
            );
        }

        public void Save(Article article) {
            throw new NotImplementedException();
        }

        public ArticleComment FindComment(int commandRootId) {
            throw new NotImplementedException();
        }
    }
}