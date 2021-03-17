
namespace Blog.Domain.AggregatesModel.Aritcle {
    public interface IArticleRepository {
        Article FindById(string articleId);
        ArticleComment FindComment(string commentId);
        void Save(Article article);
    }
}