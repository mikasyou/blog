using Blog.Domain.AggregatesModel.Aritcle;
using MongoDB.Driver;
using System;

namespace Blog.Infrastructure.Repositories {
    public class ArticleRepository : IArticleRepository {
        private readonly IMongoCollection<ArticlePO> _articles;


        public ArticleRepository(IMongoDatabase database, BlogDbOptions options) {
            _articles = database.GetCollection<ArticlePO>(options.ArticleCollectionName);
        }

        public Article FindById(string articleId) {
            var po = _articles.Find(it => it.Id == articleId).FirstOrDefault();
            if (po == null) {
                throw new NullReferenceException($"未找到指定文章,id:{articleId}");
            }

            throw new NotImplementedException();
        }

        public void Save(Article article) {
            throw new NotImplementedException();
        }

        public ArticleComment FindComment(string commandRootId) {
            throw new NotImplementedException();
        }
    }
}