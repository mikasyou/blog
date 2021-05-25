using System;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Application.Articles.Models;
using Blog.Application.Commands;
using Blog.Domain.Articles;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Utils;

namespace Blog.Application.Articles {
    public class ArticleService {
        private readonly IArticleRepository articleRepository;
        private readonly IMapper mapper;

        public ArticleService(
            IArticleRepository repository,
            IMapper mapper
        ) {
            this.articleRepository = repository;
            this.mapper = mapper;
        }


        public async Task PostComment(CreateCommentCommand command) {
            var article = await articleRepository.GetAsync(command.ArticleId);
            article.Comment(
                name: command.Name,
                webSite: command.WebSite,
                body: command.Body,
                email: command.Email,
                rootId: command.RootId,
                targetId: command.TargetId
            );
            articleRepository.Add(article);
            articleRepository.Save();
        }

        public async Task<ArticleData> ViewArticle(ViewArticleCommand command) {
            var article = await this.articleRepository.GetAsync(command.articleId);
            article.Access("TODO-IP");
            return mapper.Map<ArticleData>(article);
        }
    }
}