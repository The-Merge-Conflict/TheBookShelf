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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.IdentityUserId).IsUnique();

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);

            builder.OwnsOne(u => u.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.Street).HasMaxLength(200).HasColumnName("Street");
                addressBuilder.Property(a => a.City).HasMaxLength(100).HasColumnName("City");
                addressBuilder.Property(a => a.State).HasMaxLength(100).HasColumnName("State");
                addressBuilder.Property(a => a.ZipCode).HasMaxLength(20).HasColumnName("ZipCode");
                addressBuilder.Property(a => a.Country).HasMaxLength(100).HasColumnName("Country");
            });
        }
    }
}
