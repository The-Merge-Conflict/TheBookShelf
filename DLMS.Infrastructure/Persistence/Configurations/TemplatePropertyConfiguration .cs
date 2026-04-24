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
    public class TemplatePropertyConfiguration : IEntityTypeConfiguration<TemplateProperty>
    {
        public void Configure(EntityTypeBuilder<TemplateProperty> builder)
        {
            builder.HasKey(tp => new { tp.TemplateId, tp.PropertyId });

            builder.Property(tp => tp.AlternateLabel).HasMaxLength(100);

            builder.HasOne(tp => tp.Template)
                   .WithMany(t => t.TemplateProperties)
                   .HasForeignKey(tp => tp.TemplateId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tp => tp.Property)
                   .WithMany(p => p.TemplateProperties)
                   .HasForeignKey(tp => tp.PropertyId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
