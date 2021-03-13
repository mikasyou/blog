using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.ArticleContext;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Infrastructure.Repositories
{
    /// <summary>
    /// 文章持久化模型
    /// </summary>
    public class ArticlePO
    {
        [BsonId]
        public string Id { get; init; }

        public string       Title         { get; init; }
        public ArticleState State         { get; init; }
        public int          ReadCounts    { get; init; }
        public int          CommentCounts { get; init; }
        public DateTime     CreateDate    { get; init; } = DateTime.Now;
        public DateTime     UpdateDate    { get; init; } = DateTime.Now;
        public string       Content       { get; init; }
    }
}
