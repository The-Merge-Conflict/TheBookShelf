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
    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {
            builder.Property(m => m.StoragePath).HasMaxLength(500);
            builder.Property(m => m.FileName).HasMaxLength(255);
            builder.Property(m => m.AltText).HasMaxLength(500);

            // Value Object: MimeType
            builder.Property(m => m.MimeType)
                   .HasConversion(
                       mime => mime.Value,
                       dbString => MimeType.Create(dbString))
                   .HasMaxLength(100)
                   .HasColumnName("MimeType");

            // Value Object: FileSize
            builder.Property(m => m.FileSize)
                   .HasConversion(
                       size => size.Bytes,
                       dbLong => FileSize.Create(dbLong))
                   .HasColumnName("FileSize");

            // Value Object: FileDimensions
            builder.OwnsOne(m => m.Dimensions, dimBuilder =>
            {
                dimBuilder.Property(d => d.Width).HasColumnName("Width");
                dimBuilder.Property(d => d.Height).HasColumnName("Height");
            });

            builder.HasOne(m => m.Item)
                   .WithMany(i => i.MediaList)
                   .HasForeignKey(m => m.ItemId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
