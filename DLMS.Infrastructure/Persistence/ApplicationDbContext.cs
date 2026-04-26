using DLMS.Application.Common.Interfaces;
using DLMS.Domain.Entities;
using DLMS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Infrastructure.Persistence
{
    // IApplicationDbContext must be added first in the app layer
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<ItemSet> ItemSets => Set<ItemSet>();
        public DbSet<Media> Media => Set<Media>();

        public DbSet<Vocabulary> Vocabularies => Set<Vocabulary>();
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<Value> Values => Set<Value>();

        public DbSet<ResourceTemplate> ResourceTemplates => Set<ResourceTemplate>();
        public DbSet<TemplateProperty> TemplateProperties => Set<TemplateProperty>();

        // 4. Domain User Entity
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<IdentityRole>().HasData(
       new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
       new IdentityRole { Id = "2", Name = "Editor", NormalizedName = "EDITOR" },
       new IdentityRole { Id = "3", Name = "Viewer", NormalizedName = "VIEWER" }
   );
        }
    }
    
}
