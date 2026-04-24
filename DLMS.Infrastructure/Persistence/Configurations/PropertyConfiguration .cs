using DLMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Infrastructure.Persistence.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.Property(p => p.LocalName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Label).IsRequired().HasMaxLength(100);
            builder.Property(p => p.TermUri).IsRequired().HasMaxLength(255);

            builder.HasOne(p => p.Vocabulary)
                   .WithMany(v => v.Properties)
                   .HasForeignKey(p => p.VocabularyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
