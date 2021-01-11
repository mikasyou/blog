using AyaBlog.Models;
using AyaEntity.Command;
using AyaEntity.DataUtils;
using AyaEntity.Services;
using AyaEntity.Statement;
using Dapper;
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
    private Dictionary<string, Dictionary<string, SysDict>> dictCache
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



    public Dictionary<string, SysDict> GetTagDict(string typeCode)
    {
      if (!dictCache.TryGetValue(typeCode, out Dictionary<string, SysDict> dicts))
      {
        dicts = GetEntityList<SysDict>(new { type_code = typeCode }).ToDictionary(dict =>
         {
           return dict.Value;
         });
      }
      return dicts;
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
    /// 获取文章评论列表
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public IEnumerable<BlogComment> GetArticleComments(int articleId, int status = -1)
    {
      MysqlSelectStatement sql = new MysqlSelectStatement()
                  .Select(SqlAttribute.GetSelectColumns("floor", typeof(BlogComment),
                          SqlAttribute.GetSelectColumns("reply", typeof(BlogCommentReply))));
      sql.sortField = "floor.id";
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
      return this.Connection.Query<BlogComment, BlogCommentReply, BlogComment>
        (
          sql.From("blog_comment floor").ToSql(),
          (floor, reply) =>
          {
            // 字典里没有
            if (!floorHash.TryGetValue(floor.Id, out BlogComment floorEntry))
            {
              // 此条评论没有子回复，直接返回，不用初始化reply列表。
              if (reply == null)
              {
                return floor;
              }
              // 初始化回复列表
              floorEntry = floor;
              floorEntry.ReplyCommets = new List<BlogCommentReply>();
              floorHash.Add(floorEntry.Id, floorEntry);
            }
            // 添加子回复
            floorEntry.ReplyCommets.Add(reply);
            return floorEntry;
          }, new { articleId, status }, splitOn: "id"
        )
        .Distinct()
        .OrderBy(m => m.Id);

    }


  }
}
