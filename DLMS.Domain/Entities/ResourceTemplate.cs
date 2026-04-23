using DLMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class ResourceTemplate : BaseEntity
    {
        public string Label { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<TemplateProperty> TemplateProperties { get; set; } = new List<TemplateProperty>();
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
