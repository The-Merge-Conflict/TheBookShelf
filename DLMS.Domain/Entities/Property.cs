using DLMS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class Property : BaseEntity
    {
        public int VocabularyId { get; set; }
        //nav
        public Vocabulary Vocabulary { get; set; } = null!;
        public string LocalName { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string TermUri { get; set; } = string.Empty;

        public ICollection<Value> Values { get; set; } = new List<Value>();
        public ICollection<TemplateProperty> TemplateProperties { get; set; } = new List<TemplateProperty>();
    }
}
