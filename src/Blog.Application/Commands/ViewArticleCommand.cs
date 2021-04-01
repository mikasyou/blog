namespace Blog.Application.Commands {
    public record ViewArticleCommand(
        int articleId,
        string ip
    );
}