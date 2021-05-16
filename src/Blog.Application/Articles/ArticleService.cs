using System;
using Blog.Application.Articles.Models;
using Blog.Application.Commands;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;

namespace Blog.Application.Articles {
    public class ArticleService {
        private readonly IArticleRepository articleRepository;
        private readonly IArticleQueries queries;
        private readonly ArticleAccessService articleAccessService;

        public ArticleService(
            IArticleRepository repository,
            IArticleQueries queries,
            ArticleAccessService articleAccessService
        ) {
            this.articleRepository = repository;
            this.queries = queries;
            this.articleAccessService = articleAccessService;
        }


        public void PostComment(CreateCommentCommand command) {
            var article = articleRepository.Get(command.ArticleId)
                ?? throw new NullReferenceException($"文章不存在,article id: {command.ArticleId}");
            var comment = new ArticleComment() {
                Name = command.Name,
                Body = command.Body,
                CreateDate = DateTime.Now,
                Email = command.Email,
                RootId = command.RootId,
                TargetId = command.ParentId
            };
            article.Comment(comment);
            articleRepository.Save(article);
        }

        public ArticleData ViewArticle(ViewArticleCommand command) {
            var article = articleAccessService.AccessArticle(command.articleId, "TODO");
            var comments = queries.FindComments(article.Id);
            return new ArticleData {
                Id = article.Id,
                Title = article.Title,
                SubTitle = article.SubTitle,
                Tags = article.Tags,
                Content = article.Content,
                CreateDate = article.CreateDate,
                UpdateDate = article.UpdateDate,
                ReadCounts = article.ReadCounts,
                Comments = comments
            };
        }
    }
}