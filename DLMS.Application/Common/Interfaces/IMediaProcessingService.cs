using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Application.Common.Interfaces
{
    public interface IMediaProcessingService
    {
        long GetFileSize(IFormFile file);
        string GetMimeType(IFormFile file);
        Task<string> GenerateThumbnailAsync(IFormFile file);
    }
}
