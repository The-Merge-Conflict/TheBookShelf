using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Result, string[] Errors)> RegisterUserAsync(string email, string password, string role);
        Task<(bool Result, string Token, string[] Errors)> LoginAsync(string email, string password);
    }
}
