namespace Blog.Application.Commands {
    public record ViewArticleCommand(
        string articleId,
        string ip
    );
}
