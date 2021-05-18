using Blog.Domain.Shared.Articles;

namespace Blog.Domain.Articles {
    public interface IArticleRepository {
        Article Get(int articleId);
        ArticleComment FindComment(int commentId);

        void Save(Article article);
    }
}