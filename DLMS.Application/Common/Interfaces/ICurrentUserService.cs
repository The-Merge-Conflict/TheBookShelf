using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DLMS.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        string? UserName { get; }
    }
}