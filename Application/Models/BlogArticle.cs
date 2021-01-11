using AyaEntity.Base;
using AyaEntity.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.Models
{
  [TableName("blog_article")]
  public class BlogArticle
  {

    [PrimaryKey]
    [IdentityKey]
    [ColumnName("id")]
    public int Id { get; set; }
    /// <summary>
    /// 标识码
    /// </summary>
    [ColumnName("code")]

    public string Code { get; set; }
    [ColumnName("article_title")]
    public string Title { get; set; }

    [ColumnName("article_subtitle")]
    public string SubTitle { get; set; }
    [ColumnName("tags")]
    public string Tags { get; set; }

    [ColumnName("catalog_json")]
    public string CatalogJson { get; set; }



    [NotSelect]
    [ColumnName("content")]
    public string Content { get; set; }

    [ColumnName("subcontent")]
    public string SubContent { get; set; }
    [ColumnName("create_date")]
    public DateTime CreateDate { get; set; }



    [NotSelect]
    [NotInsert]
    public IEnumerable<BlogComment> Comments { get; set; }

    [NotSelect]
    [NotInsert]
    public IEnumerable<ArticleCatalog> Catalogs { get; set; }
  }
}
