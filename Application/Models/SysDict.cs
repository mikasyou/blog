using AyaEntity.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.Models
{
  [TableName("sys_dict")]
  public class SysDict
  {

    [IdentityKey]
    [PrimaryKey]
    public int Id { get; set; }
    
    [ColumnName("type_code")]
    public string TypeCode { get; set; }

    public string Name { get; set; }

    public string Value { get; set; }

  }
}
