using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Blog.Domain.Core;

namespace Blog.Domain.ArticleContext
{
    public enum ArticleState
    {
        Public,
        Private,
        Deleted
    }

    public class Article : DomainEntity
    {
        public string       Id            { get; }
        public string       Title         { get; init; }
        public DateTime     CreateDate    { get; init; } = DateTime.Now;
        public DateTime     UpdateDate    { get; init; }
        public string       Content       { get; init; }
        public int          ReadCounts    { get; private set; }
        public int          CommentCounts { get; private set; }
        public ArticleState State         { get; private set; }


        private List<ArticleComment> _newComments      = new();
        private List<int>            _deleteCommentsId = new();

        public Article(ArticleState initState, int initReadCounts, int initCommentCounts)
        {
            State = initState;
            ReadCounts = initReadCounts;
            CommentCounts = initCommentCounts;
        }

        public void AddComment(ArticleComment comment, ArticleComment replyComment, ArticleComment rootComment)
        {
            // 若是评论的评论，则校验要回复的评论是否存在
            if (comment.ReplyId != null)
            {
                if (replyComment == null || rootComment == null)
                    throw new InvalidDataContractException("回复的评论不存在");
                if (replyComment.Id != comment.ReplyId || rootComment.Id != comment.Id)
                    throw new InvalidDataContractException("要回复的评论，与实际评论不吻合");
            }
            // TODO: 校验评论内容是否健康

            _newComments.Add(comment);
            CommentCounts++;
        }

        public string Read()
        {
            ReadCounts++;
            return Content;
        }

        public void Delete()
        {
            State = ArticleState.Deleted;
        }
    }
}