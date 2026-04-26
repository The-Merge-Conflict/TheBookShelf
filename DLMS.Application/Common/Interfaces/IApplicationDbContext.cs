using DLMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DLMS.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Resource> Resources { get; }
        DbSet<Item> Items { get; }
        DbSet<ItemSet> ItemSets { get; }
        DbSet<Media> Media { get; }

        DbSet<Vocabulary> Vocabularies { get; }
        DbSet<Property> Properties { get; }
        DbSet<Value> Values { get; }

        DbSet<ResourceTemplate> ResourceTemplates { get; }
        DbSet<TemplateProperty> TemplateProperties { get; }

        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
