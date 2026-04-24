using DLMS.Domain.Entities;
using DLMS.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Infrastructure.Persistence.Configurations
{
    public class ValueConfiguration : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            builder.Property(v => v.ValueText).HasMaxLength(4000);
            builder.Property(v => v.ValueUri).HasMaxLength(500);

            builder.Property(v => v.Language)
                   .HasConversion(
                       lang => lang.Code,
                       dbString => LanguageCode.Create(dbString))
                   .HasMaxLength(2)
                   .HasColumnName("Language");

            builder.HasOne(v => v.Property)
                   .WithMany(p => p.Values)
                   .HasForeignKey(v => v.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(v => v.ValueResource)
                   .WithMany()
                   .HasForeignKey(v => v.ValueResourceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
