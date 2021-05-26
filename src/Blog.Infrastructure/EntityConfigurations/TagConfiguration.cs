using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Articles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.EntityConfigurations {
    class TagConfiguration : IEntityTypeConfiguration<Tag> {
        public void Configure(EntityTypeBuilder<Tag> builder) {
            builder.ToTable("tags");
            builder.HasKey(o => o.Id);
            builder.Property(it => it.Id)
                   .HasColumnType("varchar(128)");

            builder.Property(it => it.Value)
                   .IsRequired()
                   .HasColumnType("varchar(128)");
        }
    }
}