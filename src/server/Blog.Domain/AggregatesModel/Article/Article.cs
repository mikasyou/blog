using System;
using System.Collections.Generic;
using Blog.Domain.Core;
using Blog.Domain.Shared.Article;

namespace Blog.Domain.AggregatesModel.Article {
    public class Article : IDomainEntity {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int CommentCounts { get; private set; }
        public int ReadCounts { get; private set; }
        public ArticleState State { get; private set; }
        public string SubTitle { get; private set; }
        public List<ArticleTag> Tags { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }


        private List<ArticleComment> _newComments = new();

        public Article(int id, string title, int commentCounts, int readCounts, string subTitle, List<ArticleTag> tags,
                       DateTime createDate, DateTime updateDate, string content) {
            Id = id;
            Title = title;
            CommentCounts = commentCounts;
            ReadCounts = readCounts;
            SubTitle = subTitle;
            Tags = tags;
            CreateDate = createDate;
            UpdateDate = updateDate;
            Content = content;
        }

        public void AddComment(ArticleComment comment) {
            ReadCounts++;
            _newComments.Add(comment);
        }

        public void Delete() {
            State = ArticleState.Deleted;
        }
    }
}