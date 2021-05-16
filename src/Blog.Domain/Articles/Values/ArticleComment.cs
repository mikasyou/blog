namespace Blog.Domain.Articles.Values {
    public record Comment (
        string Avatar,
        string WebSite,
        string Name,
        string Email,
        string Body,
        int? TargetId,
        int? RootId
    );
}