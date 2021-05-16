using System;

namespace Blog.Domain.Shared.Articles {
    public class ArticleComment {
        public int Id { get; init; }
        public string Avatar { get; init; }
        public string WebSite { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Body { get; init; }
        public DateTime CreateDate { get; init; } = DateTime.Now;
        /// <summary>
        /// 回复评论的ID
        /// </summary>
        public int? TargetId { get; init; } = null;
        public string TargetName { get; init; } = null;
        /// <summary>
        /// 该评论附属于哪一条最先发起的评论
        /// </summary>
        public int? RootId { get; init; }
    }
}