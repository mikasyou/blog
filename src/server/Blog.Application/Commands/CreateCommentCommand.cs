namespace Blog.Application.Commands {
    public class CreateCommentCommand {
        public string Avatar { get; set; } = "default.png";
        public string WebSite { get; init; }
        public int ArticleId { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Body { get; init; }
        public int? ReplyId { get; init; } = null;
        public int RootId { get; init; }
    }
}