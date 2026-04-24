using DLMS.Domain.Common;
using DLMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Entities
{
    public class User : BaseEntity
    {
        // matching the IdentityUser in Infrastructure
        public string IdentityUserId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public Address? Address { get; set; }

    }
}
