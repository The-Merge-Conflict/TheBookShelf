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
    public class ResourceTemplateConfiguration : IEntityTypeConfiguration<ResourceTemplate>
    {
        public void Configure(EntityTypeBuilder<ResourceTemplate> builder)
        {
            builder.Property(rt => rt.Label).IsRequired().HasMaxLength(100);
            builder.Property(rt => rt.Description).HasMaxLength(500);
        }
    }
}
