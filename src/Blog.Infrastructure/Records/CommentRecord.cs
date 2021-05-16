using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Infrastructure.Models {
    [Table("comment")]
    public class CommentRecord {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(48)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(256)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(512)")]
        public string WebSite { get; set; }
        [Column(TypeName = "varcahr(1024)")]
        public string Content { get; set; }

        public int? TargetId { get; set; }
        public CommentRecord Target { get; set; }

        public int? RootId { get; set; }
        public CommentRecord Root { get; set; }

    }
}
