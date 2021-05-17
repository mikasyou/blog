namespace Blog.Application.Commands {
    public class CreateCommentCommand {
        public CreateCommentCommand(
            int articleId,
            string webSite,
            string name,
            string email,
            string body,
            int? targetId = null,
            int? rootId = null,
            string avatar = "default.png"
        ) {
            Avatar = avatar;
            ArticleId = articleId;
            WebSite = webSite;
            Name = name;
            Email = email;
            Body = body;
            TargetId = targetId;
            RootId = rootId;
        }

        public string Avatar { get; }
        public int ArticleId { get; }
        public string WebSite { get; }
        public string Name { get; }
        public string Email { get; }
        public string Body { get; }
        public int? TargetId { get; }
        public int? RootId { get; }
    }
}