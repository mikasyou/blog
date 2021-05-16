using System;
using System.Collections.Generic;
using Blog.Domain.Shared.Articles;

namespace Blog.Application.Articles.Models {
    public class ArticleIndexData {
        public ArticleIndexData(
            int id,
            string code,
            string title,
            string summary,
            string subTitle,
            IEnumerable<ArticleTag> tags,
            DateTime createDate,
            DateTime updateDate,
            int readCounts,
            int commentCounts
        ) {
            Id = id;
            Code = code;
            Title = title;
            Summary = summary;
            SubTitle = subTitle;
            Tags = tags;
            CreateDate = createDate;
            UpdateDate = updateDate;
            ReadCounts = readCounts;
            CommentCounts = commentCounts;
        }

        public int Id { get; }
        public string Code { get; }
        public string Title { get; }
        public string Summary { get; }
        public string SubTitle { get; }
        public IEnumerable<ArticleTag> Tags { get; }
        public DateTime CreateDate { get; }
        public DateTime UpdateDate { get; }
        public int ReadCounts { get; }
        public int CommentCounts { get; }
    }
}