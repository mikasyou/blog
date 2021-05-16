using System;
using System.Net;
using Blog.Application.Articles.Models;
using Blog.Application.Commands;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Utils;

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
            article.Comment(
                name: command.Name,
                webSite: command.WebSite,
                body: command.Body,
                email: command.Email,
                rootId: command.RootId,
                targetId: command.TargetId
            );
            articleRepository.Save(article);
        }

        public ArticleData ViewArticle(ViewArticleCommand command) {
            var article = articleAccessService.AccessArticle(command.articleId, "TODO");
            var comments = queries.FindComments(article.Id);
            return new ArticleData(
                id: article.Id,
                subTitle: article.SubTitle,
                code: "",
                title: article.Title,
                tags: article.Tags,
                content: article.Content,
                createDate: article.CreateDate,
                updateDate: article.UpdateDate,
                readCounts: article.ReadCounts,
                comments: comments
            );
        }
    }
}