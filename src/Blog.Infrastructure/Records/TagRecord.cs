#pragma warning disable CS8618
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Blog.Infrastructure.Records;

namespace Blog.Infrastructure.Models {
    public class TagRecord {
        [Key]
        public string ID { get; set; }
        [Column(TypeName = "varchar(128)")]
        public string Value { get; set; }
        public ICollection<ArticleRecord> Articles { get; set; }
    }
}