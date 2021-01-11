using AyaEntity.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.Models
{
  [TableName("blog_comment_reply")]
  public class BlogCommentReply
  {
    public int Id { get; set; }

    [ColumnName("floor_id")]
    public int FloorId { get; set; }
    [ColumnName("reply_id")]
    public int ReplyId { get; set; }
    [ColumnName("reply_name")]
    public string ReplyName { get; set; }

    public string Ip { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string WebSite { get; set; }
    public string Content { get; set; }
    public int Status { get; set; }
    public string Avatar { get; set; }


    [ColumnName("comment_date")]
    public DateTime CommentDate { get; set; }

    [ColumnName("reply_type")]
    public int ReplyType { get; set; }

  }
}
