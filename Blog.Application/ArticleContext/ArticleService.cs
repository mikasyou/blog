using System;
using Blog.Application.ArticleContext.Command;
using Blog.Domain.ArticleContext;

namespace Blog.Application.ArticleContext
{
    public class ArticleService
    {
        private readonly IArticleRepository _repository;

        public ArticleService(IArticleRepository repository)
        {
            _repository = repository;
        }

        public void PostComment(CreateCommentCommand command)
        {
            var article = _repository.FindById(command.ArticleId);
            var replyComment = command.ReplyId != null ? _repository.FindComment(command.ReplyId) : null;
            var rootComment = command.RootId != null ? _repository.FindComment(command.RootId) : null;
            var comment = new ArticleComment()
            {
                Name = command.Name,
                ArticleId = article.Id,
                Body = command.Body,
                CreateDate = DateTime.Now,
                Email = command.Email,
                RootId = command.RootId,
                ReplyId = command.ReplyId
            };
            article.AddComment(comment, replyComment, rootComment);
            _repository.Save(article);
        }
    }
}