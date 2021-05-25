using System;
using Blog.Domain.Shared.Utils;

namespace Blog.Domain.Articles {
    public class ArticleComment {
        public int Id { get; }
        public string Avatar { get; }
        public string WebSite { get; }
        public string Name { get; }
        public string Email { get; }
        public string Body { get; }
        public int? TargetId { get; }
        public string? TargetName { get; }
        public int? RootId { get; }

        public DateTime CreateDate { get; }

        // for ef core
        protected ArticleComment() {
            WebSite = default!;
            Name = default!;
            Email = default!;
            Body = default!;
            TargetId = default!;
            RootId = default!;
            Avatar = default!;
            CreateDate = default!;
        }

        public ArticleComment(
            string webSite,
            string name,
            string email,
            string body,
            int? targetId,
            int? rootId,
            string avatar
        ) {
            WebSite = webSite;
            Name = name;
            Email = email;
            Body = body;
            TargetId = targetId;
            RootId = rootId;
            Avatar = avatar;
            CreateDate = DateTime.Now;
            ;
        }
    }
}