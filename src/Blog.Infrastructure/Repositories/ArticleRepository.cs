using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Blog.Infrastructure.Models;
using Blog.Infrastructure.Records;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories {
    public class ArticleRepository : IArticleRepository {
        private readonly BlogDbContext database;
        private readonly IMapper mapper;

        public ArticleRepository(BlogDbContext database, IMapper mapper) {
            this.database = database;
            this.mapper = mapper;
        }

        public Article Get(int articleId) {
            var po = database.Articles.FirstOrDefault(it => it.Id == articleId)
                ?? throw new NullReferenceException($"未找到指定文章，id:{articleId}");
            database.Entry(po).State = EntityState.Detached;

            return new Article(
                id: po.Id,
                title: po.Title,
                accessCounts: po.AccessCounts,
                comments: new List<int>(),
                content: po.Content,
                subTitle: po.SubTitle,
                tags: po.Tags.Select(it => new ArticleTag(it.ID, it.Value)).ToList(),
                createDate: po.CreateDate,
                updateDate: po.UpdateDate
            );
        }

        public void Save(Article article) {
            var record = mapper.Map<ArticleRecord>(article);
            var newComments = article.GetNewComments();
            if (newComments.Any()) {
                var comments = mapper.Map<CommentRecord>(newComments);
                database.Comments.AddRange(comments);
            }

            var newAccessLog = article.GetNewAccessLog();
            if (newAccessLog.Any()) {
                var accessLogs = newAccessLog.Select(it => new ArticleAccessLogRecord {
                        ArticleId = article.Id,
                        Ip = it
                    }
                );
                database.ArticleAccessLogs.AddRange(accessLogs);
            }

            database.Articles.Update(record);
            database.SaveChanges();
        }

        public ArticleComment FindComment(int commandRootId) {
            throw new NotImplementedException();
        }
    }
}