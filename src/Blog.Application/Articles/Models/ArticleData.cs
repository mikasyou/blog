using System;
using System.Collections.Generic;
using Blog.Domain.Shared.Articles;

namespace Blog.Application.Articles.Models {
    public class ArticleData {
        public ArticleData(
            int id,
            string code,
            string title,
            string content,
            string subTitle,
            IEnumerable<ArticleTag> tags,
            DateTime createDate,
            DateTime updateDate,
            int accessCounts,
            List<ArticleComment> comments
        ) {
            Id = id;
            Code = code;
            Title = title;
            Content = content;
            SubTitle = subTitle;
            Tags = tags;
            CreateDate = createDate;
            UpdateDate = updateDate;
            AccessCounts = accessCounts;
            Comments = comments;
        }

        public int Id { get; }
        public string Code { get; }
        public string Title { get; }
        public string Content { get; }
        public string SubTitle { get; }
        public IEnumerable<ArticleTag> Tags { get; }
        public DateTime CreateDate { get; }
        public DateTime UpdateDate { get; }
        public int AccessCounts { get; }
        public List<ArticleComment> Comments { get; }
    }
}