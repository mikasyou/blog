using System;
using Blog.Domain.Core;

namespace Blog.Domain.AggregatesModel.Article {
    public class ArticleComment : IDomainEntity {
        public int Id { get; init; }
        public int ArticleId { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Body { get; init; }
        public DateTime CreateDate { get; init; } = DateTime.Now;


        /// <summary>
        /// 回复评论的ID
        /// </summary>
        public int? ReplyId { get; init; } = null;

        /// <summary>
        /// 该评论附属于哪一条最先发起的评论
        /// </summary>
        public int RootId { get; init; }
    }
}