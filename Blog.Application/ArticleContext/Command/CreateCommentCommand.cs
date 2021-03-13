namespace Blog.Application.ArticleContext.Command
{
    public class CreateCommentCommand
    {
        public string ArticleId { get; init; }
        public string Name      { get; init; }
        public string Email     { get; init; }
        public string Body      { get; init; }
        public string ReplyId   { get; init; } = null;
        public string RootId    { get; init; }
    }
}