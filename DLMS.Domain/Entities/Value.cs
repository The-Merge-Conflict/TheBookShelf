using DLMS.Domain.Common;
using DLMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class Value : BaseEntity
    {
        public int ResourceId { get; set; }
        public Resource Resource { get; set; } = null!;

        public int PropertyId { get; set; }
        public Property Property { get; set; } = null!;

        public ValueType ValueType { get; set; }

        public string? ValueText { get; set; }
        public string? ValueUri { get; set; }

        // Internal Linking to another Resource
        public int? ValueResourceId { get; set; }
        public Resource? ValueResource { get; set; }
        public LanguageCode? Language { get; set; }
    }
}
