using DLMS.Domain.Entities;
using DLMS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Infrastructure.Persistence.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasDiscriminator(r => r.ResourceType)
                   .HasValue<Item>(ResourceType.Item)
                   .HasValue<ItemSet>(ResourceType.ItemSet)
                   .HasValue<Media>(ResourceType.Media);

            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.ModifiedBy).HasMaxLength(100);
            builder.Property(r => r.OwnerId).HasMaxLength(450);

            builder.HasMany(r => r.Values)
                   .WithOne(v => v.Resource)
                   .HasForeignKey(v => v.ResourceId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
