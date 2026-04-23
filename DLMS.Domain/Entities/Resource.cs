using DLMS.Domain.Common;
using DLMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public abstract class Resource : BaseEntity, IAuditableEntity
    {
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public string? OwnerId { get; set; }

        public ResourceType ResourceType { get; init; }

        public ICollection<Value> Values { get; set; } = new List<Value>();
    }
}
