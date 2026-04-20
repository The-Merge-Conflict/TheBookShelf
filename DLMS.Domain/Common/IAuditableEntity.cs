using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.Common
{
    public interface IAuditableEntity
    {
        string? CreatedBy { get; set; }
        string? ModifiedBy { get; set; }
    }
}
