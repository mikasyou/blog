using System;

namespace Blog.Domain.ArticleContext
{
    public class ArticleComment
    {
        public string   Id         { get; init; }
        public string   ArticleId  { get; init; }
        public string   Name       { get; init; }
        public string   Email      { get; init; }
        public string   Body       { get; init; }
        public DateTime CreateDate { get; set; } = DateTime.Now;


        /// <summary>
        /// 回复评论的ID
        /// </summary>
        public string ReplyId { get; init; } = null;

        /// <summary>
        /// 我们不采用楼中楼评论，采用列表形式。
        /// 需要此ID代表该评论附属于哪一条最先发起的评论
        /// </summary>
        public string RootId { get; init; }
    }
}