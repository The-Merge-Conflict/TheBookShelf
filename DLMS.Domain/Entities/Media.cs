using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class Media : Resource
    {
        public int ItemId { get; set; }
        //nav
        public Item Item { get; set; } = null!;
        public string StoragePath { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string MimeType { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? AltText { get; set; }
    }
}
