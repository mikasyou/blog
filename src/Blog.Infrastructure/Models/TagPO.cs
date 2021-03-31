using System.Collections.Generic;

namespace Blog.Infrastructure.Models {
    public class TagPO {
        public string ID { get; set; }
        public string Value { get; set; }
        public ICollection<ArticlePO> Articles { get; set; }
    }
}