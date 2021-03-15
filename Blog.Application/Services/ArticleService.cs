using System;
using System.Runtime.Serialization;
using Blog.Domain.AggregatesModel.Aritcle;

namespace Blog.Application.ArticleContext {
    public class ArticleService {

        IArticleRepository _repository;

        public ArticleService(IArticleRepository repository) {
            _repository = repository;
        }


        public void PostComment(CreateCommentCommand command) {
            var article = _repository.FindById(command.ArticleId) ??
                throw new NullReferenceException($"文章不存在,article id: {command.ArticleId}");

            // 若是评论的评论，则校验要回复的评论是否存在
            if (command.ReplyId != null && _repository.FindComment(command.ReplyId) == null)
                throw new InvalidDataContractException("回复的评论不存在");


            article.AddComment(comment);
            _repository.Save(article);
        }


    }
}