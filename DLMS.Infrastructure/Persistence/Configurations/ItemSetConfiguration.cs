using DLMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DLMS.Infrastructure.Persistence.Configurations
{
    public class ItemSetConfiguration : IEntityTypeConfiguration<ItemSet>
    {
        public void Configure(EntityTypeBuilder<ItemSet> builder)
        {
            builder.Property(s => s.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(s => s.Description)
                   .HasMaxLength(1000);

            builder.HasMany(s => s.Items)
                   .WithMany(i => i.ItemSets)
                   .UsingEntity(j => j.ToTable("ItemItemSet"));
        }
    }
}
