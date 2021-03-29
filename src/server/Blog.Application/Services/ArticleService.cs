using System;
using System.Runtime.Serialization;
using Blog.Application.Commands;
using Blog.Application.Models;
using Blog.Domain.AggregatesModel.Article;

namespace Blog.Application.Services {
    public class ArticleService {
        private IArticleRepository _repository;

        public ArticleService(IArticleRepository repository) {
            _repository = repository;
        }


        public void PostComment(CreateCommentCommand command) {
            var article = _repository.FindById(command.ArticleId)
                ?? throw new NullReferenceException($"文章不存在,article id: {command.ArticleId}");

            // 若是评论的评论，则校验要回复的评论是否存在
            if (command.ReplyId != null && _repository.FindComment(command.ReplyId.Value) == null)
                throw new InvalidDataContractException($"回复的评论不存在, reply comment id: {command.ReplyId}");

            var comment = new ArticleComment() {
                Name = command.Name,
                ArticleId = command.ArticleId,
                Body = command.Body,
                CreateDate = DateTime.Now,
                Email = command.Email,
                RootId = command.RootId,
                ReplyId = command.ReplyId
            };
            article.AddComment(comment);
            _repository.Save(article);
        }

        public ArticleTO ViewArticle(ViewArticleCommand command) {
            throw new NotImplementedException();
        }
    }
}