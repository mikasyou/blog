using System;
using System.Collections.Generic;
using Blog.Domain.Core;

namespace Blog.Domain.AggregatesModel.Aritcle {
    public enum ArticleState {
        Public,
        Private,
        Deleted
    }


    public class ArticleTag {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class Article : IDomainEntity {
        public string Id { get; private set; }
        public string Title { get; private set; }
        public int ReadCounts { get; private set; }
        public int CommentCounts { get; private set; }
        public ArticleState State { get; private set; }
        public string SubTitle { get; private set; }
        public List<ArticleTag> Tags { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }


        private List<ArticleComment> _newComments = new();

        public Article(string id) {
            Id = id;
        }

        public void AddComment(ArticleComment comment) {
            this.ReadCounts++;
            this._newComments.Add(comment);
        }

        public void Delete() {
            State = ArticleState.Deleted;
        }
    }
}