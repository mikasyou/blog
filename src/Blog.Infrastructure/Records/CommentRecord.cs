using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Infrastructure.Models {
    [Table("comment")]
    public class CommentRecord {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(48)")]
#pragma warning disable CS8618
        public string Name { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(512)")]
        public string WebSite { get; set; }
        [Column(TypeName = "varchar(1024)")]
        public string Content { get; set; }
#pragma warning restore CS8618
        public int ArticleId { get; set; }
        public int? TargetId { get; set; }
        public CommentRecord? Target { get; set; }
        public int? RootId { get; set; }


    }
}
