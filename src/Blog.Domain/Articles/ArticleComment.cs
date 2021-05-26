using System;
using Blog.Domain.Shared.Utils;

namespace Blog.Domain.Articles {
    public class ArticleComment {
        public int Id { get; private set; }
        public string Avatar { get; private set; }
        public string WebSite { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Body { get; private set; }
        public int? TargetId { get; private set; }
        public string? TargetName { get; private set; }
        public int? RootId { get; private set; }

        public DateTime CreateDate { get; private set; }

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
        }
    }
}