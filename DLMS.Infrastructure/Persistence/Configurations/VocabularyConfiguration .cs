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
    public class VocabularyConfiguration : IEntityTypeConfiguration<Vocabulary>
    {
        public void Configure(EntityTypeBuilder<Vocabulary> builder)
        {
            // Prevent duplicate vocabularies
            builder.HasIndex(v => v.Prefix).IsUnique();

            builder.Property(v => v.Prefix).IsRequired().HasMaxLength(50);
            builder.Property(v => v.NamespaceUri).IsRequired().HasMaxLength(255);
            builder.Property(v => v.Label).IsRequired().HasMaxLength(100);
        }
    }
}
