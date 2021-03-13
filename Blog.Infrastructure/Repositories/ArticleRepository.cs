using System;
using System.Collections.Generic;
using Blog.Domain.ArticleContext;
using MongoDB.Driver;

namespace Blog.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private IMongoCollection<ArticlePO> _articles;


        public ArticleRepository(IMongoDatabase database, IMongoDbSettings settings)
        {
            _articles = database.GetCollection<ArticlePO>(settings.ArticleCollectionName);
        }

        public IEnumerable<Article> ListArticle(int? limit, int? offset)
        {
            throw new NotImplementedException();
        }

        public Article FindById(string articleId)
        {
            var po = _articles.Find(it => it.Id == articleId).FirstOrDefault();
            if (po == null)
                throw new NullReferenceException($"未找到指定文章,id:{articleId}");
            return new Article(po.State, po.ReadCounts, po.CommentCounts)
            {
                Title = po.Title,
                Content = po.Content,
                CreateDate = po.CreateDate,
                UpdateDate = po.UpdateDate
            };
        }

        public void Save(Article article)
        {
            throw new NotImplementedException();
        }

        public ArticleComment FindComment(string commandRootId)
        {
            throw new NotImplementedException();
        }
    }
}