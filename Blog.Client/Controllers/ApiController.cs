using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Client.Controllers
{

  [Route("api")]
  [ApiController]
  public class BlogController : ControllerBase
  {
    BlogDbService blogService;

    public ApiBlogController(SqlManager manager)
    {
      blogService = manager.UseService<BlogDbService>();
    }


    public const string Gravatar_Url = "https://www.gravatar.com/avatar/";
    public static string GetGravatarImage(string email)
    {
      string hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Default.GetBytes(email.ToLower()))).Replace("-", "").ToLower();
      return Gravatar_Url + hash;
    }


    public object Success(object data)
    {
      return new { result = 0, data };
    }

    [HttpPost("postComment")]
    public object PostComment(JObject param)
    {
      BlogComment comment = new BlogComment
      {
        Name = param.Value<string>("name"),
        Email = param.Value<string>("email"),
        WebSite = param.Value<string>("website"),
        Content = param.Value<string>("content"),
        ArticleId = param.Value<int>("articleid"),
        CommentDate = DateTime.Now,
        Ip = Request.HttpContext.GetAccessUserIp(),
        Status = 0
      };
      // 设置头像
      if (string.IsNullOrEmpty(comment.Email))
      {
        return (new { msg = "邮箱不能为空" });
      }
      comment.Avatar = GetGravatarImage(comment.Email);
      int i = blogService.Insert(comment);
      if (i == 1)
      {
        Response.Cookies.Append("name", comment.Name, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
        Response.Cookies.Append("email", comment.Email, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
        Response.Cookies.Append("website", comment.WebSite, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
        return Success("");
      }
      else
      {
        return BadRequest();
      }
    }

    [HttpPost("postComment/reply")]
    public object PostCommentReply(JObject param)
    {
      try
      {
        BlogCommentReply reply = new BlogCommentReply
        {
          Name = param.Value<string>("name"),
          Ip = Request.HttpContext.GetAccessUserIp(),
          Email = param.Value<string>("email"),
          WebSite = param.Value<string>("website"),
          Content = param.Value<string>("content"),
          CommentDate = DateTime.Now,
          FloorId = param.Value<int>("floorid"),
          ReplyId = param.Value<int>("replyid"),
          ReplyName = param.Value<string>("replyname"),
          ReplyType = param.Value<int>("type"),
          Status = 0
        };

        // 设置头像
        if (string.IsNullOrEmpty(reply.Email))
        {
          return BadRequest(new { msg = "邮箱不能为空" });
        }
        // 设置头像
        reply.Avatar = GetGravatarImage(reply.Email);
        reply.Id = blogService.Insert(reply, true);
        if (reply.Id > 0)
        {
          Response.Cookies.Append("name", reply.Name, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
          Response.Cookies.Append("email", reply.Email, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });
          Response.Cookies.Append("website", reply.WebSite, new CookieOptions { Expires = DateTime.Now.AddMonths(1) });

          return Success("");
        }
        else
        {
          return BadRequest();
        }
      }
      catch
      {
        return BadRequest();
      }
    }
  }
}