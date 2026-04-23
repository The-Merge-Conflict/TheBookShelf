using DLMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class Vocabulary : BaseEntity
    {
        public string Prefix { get; set; } = string.Empty;
        public string NamespaceUri { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
