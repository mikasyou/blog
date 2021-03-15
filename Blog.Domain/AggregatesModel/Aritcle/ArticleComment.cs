using System;
using System.Runtime.Serialization;
using Blog.Domain.Core;

namespace Blog.Domain.AggregatesModel.Aritcle {
    public class ArticleComment : IDomainEntity {
        public string Id { get; init; }
        public string ArticleId { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Body { get; init; }
        public DateTime CreateDate { get; init; } = DateTime.Now;


        /// <summary>
        /// 回复评论的ID
        /// </summary>
        public string ReplyId { get; init; } = null;

        /// <summary>
        /// 该评论附属于哪一条最先发起的评论
        /// </summary>
        public string RootId { get; init; }


        public static ArticleComment Create(CreateCommentCommand command) {
            return new() {
                Name = command.Name,
                ArticleId = command.ArticleId,
                Body = command.Body,
                CreateDate = DateTime.Now,
                Email = command.Email,
                RootId = command.RootId,
                ReplyId = command.ReplyId
            };
        }
    }
}