namespace Blog.Domain.AggregatesModel.Article {
    public interface IArticleRepository {
        Article FindById(string articleId);
        ArticleComment FindComment(string commentId);
        void Save(Article article);
    }
}