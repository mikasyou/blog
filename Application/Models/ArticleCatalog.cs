using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AyaBlog.Models
{
  public class ArticleCatalog
  {
    public string Text { get; set; }
    public string Index { get; set; }
    public string Anchor { get; set; }
    public IEnumerable<ArticleCatalog> Child { get; set; }
  }
}
