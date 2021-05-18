using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Core;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Exceptions;
using Blog.Domain.Shared.Utils;

namespace Blog.Domain.Articles {
    public class Article : IDomainAggragationRoot {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public List<int> Comments { get; set; }
        public string Content { get; private set; }

        public int CommentCounts => newComments.Count + Comments.Count;
        public int AccessCount { get; private set; }
        public ArticleState State { get; private set; }
        public string SubTitle { get; private set; }
        public List<ArticleTag> Tags { get; private set; }
        public DateTime CreateDate { get; private set; }

        public DateTime UpdateDate { get; private set; }


        private readonly List<Values.Comment> newComments = new();
        private readonly List<string> newAccessLogs = new();

        public IEnumerable<Values.Comment> GetNewComments() {
            return newComments;
        }

        public IEnumerable<string> GetNewAccessLog() {
            return newAccessLogs;
        }

        public Article(
            int id,
            string title,
            string subTitle,
            List<ArticleTag> tags,
            DateTime createDate,
            DateTime updateDate,
            string content,
            List<int> comments,
            int accessCounts
        ) {
            Id = id;
            this.AccessCount = accessCounts;
            Title = title;
            SubTitle = subTitle;
            Tags = tags;
            CreateDate = createDate;
            UpdateDate = updateDate;
            Content = content;
            this.Comments = comments;
        }

        public void Access(string ip) {
            AccessCount++;
            newAccessLogs.Add(ip);
        }


        public void Comment(
            string webSite,
            string name,
            string email,
            string body,
            int? rootId,
            int? targetId
        ) {
            // 若是评论的评论，则校验要回复的评论是否存在
            if (targetId != null && Comments.Contains(targetId.Value)) {
                throw DomainException.Illogical($"回复的评论不存在, reply comment id: {targetId}");
            }

            var comment = new Values.Comment(
                Name: name,
                Avatar: DataTools.MakeGravatarImage(email),
                WebSite: webSite,
                Body: body,
                Email: email,
                RootId: rootId,
                TargetId: targetId
            );
            this.newComments.Add(comment);
        }

        public void Delete() {
            State = ArticleState.Deleted;
        }
    }
}