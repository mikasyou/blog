using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Core;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Exceptions;

namespace Blog.Domain.Articles {
    public class Article : IDomainAggragationRoot {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<int> Comments { get; set; }
        public string Content { get; private set; }
        public int CommentCounts => Comments.Count();
        public int ReadCounts { get; private set; }
        public ArticleState State { get; private set; }
        public string SubTitle { get; private set; }
        public List<ArticleTag> Tags { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }


        public Article(
            int id,
            string title,
            int readCounts,
            string subTitle,
            List<ArticleTag> tags,
            DateTime createDate,
            DateTime updateDate,
            string content,
            IEnumerable<int> comments
        ) {
            Id = id;
            Title = title;
            ReadCounts = readCounts;
            SubTitle = subTitle;
            Tags = tags;
            CreateDate = createDate;
            UpdateDate = updateDate;
            Content = content;
            this.Comments = comments;
        }

        public void Access(string ip) {
            throw new NotImplementedException();
        }

        public void Comment(ArticleComment comment) {
            // 若是评论的评论，则校验要回复的评论是否存在
            if (comment.TargetId != null && Comments.Contains(comment.TargetId.Value)) {
                throw DomainException.Illogic($"回复的评论不存在, reply comment id: {comment.TargetId}");
            }
        }

        public void Delete() {
            State = ArticleState.Deleted;
        }
    }
}