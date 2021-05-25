using System.Threading.Tasks;

namespace Blog.Domain.Articles {
    public class ArticleAccessService {
        private readonly IArticleRepository articleRepository;

        public ArticleAccessService(IArticleRepository articleRepository) {
            this.articleRepository = articleRepository;
        }

        public async Task<Article> AccessArticle(int articleId, string ip) {
            var article = await this.articleRepository.GetAsync(articleId);
            article.Access(ip);
            return article;
        }
    }
}