namespace Blog.Domain.AggregatesModel.Article {
    public interface IArticleRepository {
        Article FindById(int articleId);
        ArticleComment FindComment(int commentId);
        void Save(Article article);
    }
}