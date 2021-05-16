using System.Collections.Generic;

namespace Blog.Infrastructure.Models {
    public class TagRecord {
        public string ID { get; set; }
        public string Value { get; set; }
        public ICollection<ArticleRecord> Articles { get; set; }
    }
}