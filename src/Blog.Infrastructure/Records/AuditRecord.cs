#pragma warning disable CS8618
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Infrastructure.Records {

    public class AuditRecord {
        [Key]
        public int Id { get; init; }
        [Column(TypeName = "varchar(128)")]
        public string Ip { get; init; }
        public DateTime CreateDate { get; init; }
    }
}
