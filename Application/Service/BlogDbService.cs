using AKiuLog;
using AKiuLog.Message;
using AyaBlog.Models;
using AyaEntity.Command;
using AyaEntity.DataUtils;
using AyaEntity.Services;
using AyaEntity.Statement;
using Dapper;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.DaoService
{
  public class BlogDbService : DBService
  {
    Dictionary<string, Dictionary<string, SysDict>> _dictCache;
    private Dictionary<string, Dictionary<string, SysDict>> DictCache
    {
      get
      {
        if (_dictCache == null)
        {
          _dictCache = new Dictionary<string, Dictionary<string, SysDict>>();
        }
        return _dictCache;
      }
      set
      {
        this._dictCache = value;
      }
    }

    public BlogDbService()
    {
    }



    /// <summary>
    /// 获取数据字典，带缓存处理
    /// </summary>
    /// <param name="typeCode"></param>
    /// <returns></returns>
    public Dictionary<string, SysDict> GetSysDict(string typeCode)
    {
      if (!DictCache.TryGetValue(typeCode, out Dictionary<string, SysDict> dicts))
      {
        dicts = GetEntityList<SysDict>(new { type_code = typeCode }).ToDictionary(dict =>
        {
          return dict.Value;
        });
      }
      return dicts;
    }

    /// <summary>
    /// 评论有回复，邮件通知
    /// </summary>
    public void ReplyEmailNotify(BlogCommentReply reply)
    {
      try
      {

        BlogComment comment = this.GetEntity<BlogComment>(new { Id = reply.FloorId }); ;
        // 回复顶层
        if (reply.ReplyId != reply.FloorId)
        {
          BlogCommentReply replied = this.GetEntity<BlogCommentReply>(new { Id = reply.ReplyId });
          comment.Name = replied.Name;
          comment.CommentDate = replied.CommentDate;
          comment.Content = replied.Content;
        }
        string code = this.Connection.ExecuteScalar<string>(new MysqlSelectStatement().Select("code").From("blog_article").Where("id=@id").ToSql(), new { id = comment.ArticleId });
        var messageToSend = new MimeMessage
        {
          Sender = new MailboxAddress("Kaakira", "Kaakira@Outlook.com"),
          Subject = @"Kaakira Blog评论回复通知",
          Body = new TextPart(TextFormat.Html)
          {
            Text = $@"
                    <div>
                        <p style='color:#808080;font-size: 0.8em'>ps：自动通知邮件，无需回复</p>
                        <p>{comment.Name} 带佬，你在kakira blog的评论有了一条新回复，</p>
                        <p>你当时(<small>{comment.CommentDate.ToString("yyyy-MM-dd HH:mm:ss")}</small>)说了：</p>
                        <p>{comment.Content}</p>
                        <p>{reply.Name}回复你：</p>
                        <p>{reply.Content}</p>
                        <p><a href='{"https://kaakira.com/article/"}{code}/{comment.ArticleId}#comment-reply-{reply.Id}'>可以来瞧瞧~点击这里，查看评论！</a></p>
                    </div>"
          }
        };
        messageToSend.To.Add(new MailboxAddress(comment.Name, comment.Email));
        using (var smtp = new SmtpClient())
        {
          smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
          smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
          smtp.Authenticate("Kaakira@Outlook.com", "25912591dian");

          smtp.Send(messageToSend);

          smtp.Disconnect(true);
        }
      }
      catch (Exception ex)
      {
        AKiuLogErrorMessage log = new AKiuLogErrorMessage(ex);
        log.SetColumns<AKiuLogSaveFile>("发送回复通知邮件出现异常：" + ex.Message);
        AKiuLogger.Logger().WriteLog(log);
      }
    }



    /// <summary>
    /// 查看文章
    /// </summary>
    /// <param name="conditionParameters"></param>
    /// <returns></returns>
    public BlogArticle GetArticleWithView(object conditionParameters)
    {
      Type entityType = typeof(BlogArticle);
      BlogArticle article = this.Connection.QueryFirstOrDefault<BlogArticle>(CommandBuilder.BuildSelect(null, entityType)
                      .Select(SqlAttribute.GetSelectColumns(entityType, "content as Content"))
                      .From("blog_article")
                      .Where(conditionParameters)
                      .ToSql(), conditionParameters);
      article.Comments = GetArticleComments(article.Id, 1);
      if (string.IsNullOrEmpty(article.CatalogJson))
      {
        article.Catalogs = new ArticleCatalog[0];
      }
      else
      {
        article.Catalogs = JsonConvert.DeserializeObject<ArticleCatalog[]>(article.CatalogJson);
      }
      return article;
    }



    /// <summary>
    /// 获取文章评论
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public IEnumerable<BlogComment> GetArticleComments(int articleId, int status = -1)
    {
      MysqlSelectStatement sql = new MysqlSelectStatement()
                  .Select(SqlAttribute.GetSelectColumns("floor", typeof(BlogComment),
                          SqlAttribute.GetSelectColumns("reply", typeof(BlogCommentReply))));
      sql.sortField = "floor.id,reply.id";
      sql.sortType = SortType.ASC;
      // 评论状态
      if (status == -1)
      {
        sql.Join("LEFT JOIN blog_comment_reply reply on floor.id=reply.floor_id")
           .Where("floor.article_id=@articleId");
      }
      else
      {
        sql.Join("LEFT JOIN blog_comment_reply reply on floor.id=reply.floor_id and reply.status={=status}")
           .Where("floor.article_id=@articleId and floor.status={=status}");
      }


      Dictionary<int, BlogComment> floorHash = new Dictionary<int, BlogComment>();
      // dapper多表查询处理
      return this.Connection.Query<BlogComment, BlogCommentReply, BlogComment>(
          sql.From("blog_comment floor").ToSql(),
          (floor, reply) =>
          {
            // 字典里没有
            if (!floorHash.TryGetValue(floor.Id, out BlogComment floorEntry))
            {
              if (reply == null)
              {
                return floor;
              }
              floorEntry = floor;
              floorEntry.ReplyCommets = new List<BlogCommentReply>();
              floorHash.Add(floorEntry.Id, floorEntry);
            }
            floorEntry.ReplyCommets.Add(reply);
            return floorEntry;
          }, new { articleId, status }, splitOn: "id")
          .Distinct()
          .OrderBy(m => m.Id);

    }


  }
}
