using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories {
    public class ArticleRepository : IArticleRepository {
        private readonly BlogDbContext database;


        public ArticleRepository(BlogDbContext database) {
            this.database = database;
        }

        public Article Get(int articleId) {
            var po = database.Articles.FirstOrDefaultAsync(it => it.ID == articleId).Result;
            if (po == null)
                throw new NullReferenceException($"未找到指定文章，id:{articleId}");

            return new Article(
                id: po.ID,
                title: po.Title,
                comments: new List<int>(),
                readCounts: po.AccessCounts,
                content: po.Content,
                subTitle: po.SubTitle,
                tags: po.Tags.Select(it => new ArticleTag(it.ID, it.Value)).ToList(),
                createDate: po.CreateDate,
                updateDate: po.UpdateDate
            );
        }

        public void Save(Article article) {
            throw new NotImplementedException();
        }


        public ArticleComment FindComment(int commandRootId) {
            throw new NotImplementedException();
        }

        public void Access(int id, string ip) {
            throw new NotImplementedException();
        }
    }
}