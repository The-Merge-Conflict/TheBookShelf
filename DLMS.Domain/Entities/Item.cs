using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class Item : Resource
    {
        public int? TemplateId { get; set; }
        public ResourceTemplate? Template { get; set; }

        public ICollection<Media> MediaList { get; set; } = new List<Media>();
        public ICollection<ItemSet> ItemSets { get; set; } = new List<ItemSet>();
    }
}
