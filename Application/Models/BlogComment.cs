using AyaEntity.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.Models
{
  [TableName("blog_comment")]
  public class BlogComment
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string WebSite { get; set; }
    public string Content { get; set; }
    public string Avatar { get; set; }

    public string Ip { get; set; }

    public int Status { get; set; }
    [ColumnName("article_id")]
    public int ArticleId { get; set; }



    [ColumnName("comment_date")]
    public DateTime CommentDate { get; set; }


    [NotSelect]
    [NotInsert]
    public List<BlogCommentReply> ReplyCommets { get; set; }
  }
}
