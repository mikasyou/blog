using AyaEntity.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.Models
{
  /// <summary>
  /// 数据字典表
  /// </summary>
  [TableName("blog_data_dict")]
  public class BlogDataDict
  {
    [PrimaryKey]
    [IdentityKey]
    public int Id { get; set; }

    [ColumnName("dict_code")]
    public string DictCode { get; set; }

    public int Key { get; set; }

    public string Value { get; set; }

    [ColumnName("create_date")]
    public DateTime CreateDate { get; set; }
  }
}
