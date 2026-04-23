using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class TemplateProperty
    {
        public int TemplateId { get; set; }
        //nav
        public ResourceTemplate Template { get; set; } = null!;

        public int PropertyId { get; set; }
        //nav
        public Property Property { get; set; } = null!;

        public bool IsRequired { get; set; }
        public int DisplayOrder { get; set; }
        public string? AlternateLabel { get; set; }
    }
}
