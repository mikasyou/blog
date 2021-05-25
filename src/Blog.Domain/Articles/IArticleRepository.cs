using System.Threading.Tasks;
using Blog.Domain.Shared.Articles;

namespace Blog.Domain.Articles {
    public interface IArticleRepository {
        Task<Article> GetAsync(int articleId);

        Article Add(Article order);

        void Save();
    }
}