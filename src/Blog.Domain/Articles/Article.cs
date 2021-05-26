using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Domain.Core;
using Blog.Domain.Shared.Articles;
using Blog.Domain.Shared.Exceptions;
using Blog.Domain.Shared.Utils;

namespace Blog.Domain.Articles {
    public class Article : DomainEntity {
        public string Title { get; private set; }
        public List<ArticleComment> Comments { get; private set; }
        public string Content { get; private set; }

        public string Summary { get; private set; }
        public ArticleState State { get; private set; }
        public string SubTitle { get; private set; }
        public List<Tag> Tags { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public string Code { get; private set; }

        protected Article() {
            Title = default!;
            Comments = default!;
            Content = default!;
            SubTitle = default!;
            Tags = default!;
            Summary = default!;
            Code = default!;
        }


        public Article(
            string title,
            string subTitle,
            List<Tag> tags,
            DateTime createDate,
            DateTime updateDate,
            string content,
            string summary,
            string code,
            List<ArticleComment> comments
        ) {
            Title = title;
            SubTitle = subTitle;
            Tags = tags;
            CreateDate = createDate;
            UpdateDate = updateDate;
            Content = content;
            Code = code;
            Summary = summary;
            this.Comments = comments;
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
            if (targetId != null && Comments.All(it => it.Id != targetId.Value)) {
                throw DomainException.Illogical($"回复的评论不存在, reply comment id: {targetId}");
            }

            var comment = new ArticleComment(
                name: name,
                avatar: DataTools.MakeGravatarImage(email),
                webSite: webSite,
                body: body,
                email: email,
                rootId: rootId,
                targetId: targetId
            );
            this.Comments.Add(comment);
        }

        public void Delete() {
            State = ArticleState.Deleted;
        }

        public void Access(string ip) {
            throw new NotImplementedException();
        }
    }
}