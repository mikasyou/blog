using System;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Shared.Articles {
    public class ArticleComment {
        private readonly int? id;

        public ArticleComment(
            string avatar,
            string webSite,
            string name,
            string email,
            string body,
            int? id,
            int? targetId
        ) {
            this.id = id;
            TargetId = targetId;
            Avatar = avatar;
            WebSite = webSite;
            Name = name;
            Email = email;
            Body = body;
        }

        public int Id => this.id ?? throw DomainException.NotPersistent(nameof(ArticleComment));

        public string Avatar { get; }
        public string WebSite { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Body { get; init; }
        public DateTime CreateDate { get; init; } = DateTime.Now;

        /// <summary>
        /// 该评论附属于哪一条最先发起的评论
        /// </summary>
        public int? RootId { get; init; }

        /// <summary>
        /// 回复评论的ID
        /// </summary>
        public int? TargetId { get; init; }

        public string? TargetName { get; init; }
    }
}