namespace Blog.Domain.Articles {
    public class ArticleAccessService {
        private readonly IArticleRepository articleRepository;

        public ArticleAccessService(IArticleRepository articleRepository) {
            this.articleRepository = articleRepository;
        }

        public Article AccessArticle(int articleId, string ip) {
            var article = this.articleRepository.Get(articleId);
            article.Access(ip);
            return article;
        }
    }
}