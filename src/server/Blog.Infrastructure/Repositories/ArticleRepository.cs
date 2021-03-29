using System;
using Blog.Domain.AggregatesModel.Article;
using Blog.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories {
    public class ArticleRepository : IArticleRepository {
        private readonly DbSet<ArticlePO> _articles;


        public ArticleRepository(BlogDbContext database, BlogDbOptions options) {
            _articles = database.Articles;
        }

        public Article FindById(int articleId) {
            var po = _articles.FirstOrDefaultAsync(it => it.ID == articleId).Result;
            if (po == null) throw new NullReferenceException($"未找到指定文章，id:{articleId}");

            throw new NotImplementedException();
        }

        public void Save(Article article) {
            throw new NotImplementedException();
        }

        public ArticleComment FindComment(int commandRootId) {
            throw new NotImplementedException();
        }
    }
}