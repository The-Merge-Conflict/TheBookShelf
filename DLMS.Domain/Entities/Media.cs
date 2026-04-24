using DLMS.Domain.ValueObjects;
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
        public string? AltText { get; set; }

        public MimeType MimeType { get; set; } = null!;
        public FileSize FileSize { get; set; } = null!;
        public FileDimensions? Dimensions { get; set; }

    }
}
