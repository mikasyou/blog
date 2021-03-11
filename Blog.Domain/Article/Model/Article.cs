using Blog.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Domain.Article
{
    public enum ArticleState
    {
        Public,
        Private,
        Deleted
    }

    class Article : DomainEntity
    {
        public string Title { get; private set; }
        IEnumerable<CommentOfArticle> Comments { get; set; }
        public ArticleState State { get; private set; }
        public int ReadCounts { get; private set; }
        string Content { get; set; }

        public String Read()
        {
            this.ReadCounts++;
            return Content;
        }

        public void Delete()
        {
            this.State = ArticleState.Deleted;
        }
    }
}
