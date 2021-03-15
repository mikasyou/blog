namespace Blog.Application.Commands {
    public class CreateCommentCommand {
        public string Avatar { get; set; } = "default.png";
        public string WebSite { get; init; }
        public string ArticleId { get; init; }
        public string Name { get; init; }
        public string Email { get; init; }
        public string Body { get; init; }
        public string ReplyId { get; init; } = null;
        public string RootId { get; init; }
    }
}