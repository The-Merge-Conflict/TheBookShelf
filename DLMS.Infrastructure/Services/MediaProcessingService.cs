using DLMS.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Infrastructure.Services
{
    public class MediaProcessingService : IMediaProcessingService
    {
        public long GetFileSize(IFormFile file)
        {
            return file.Length;
        }

        public string GetMimeType(IFormFile file)
        {
            return file.ContentType;
        }

        public async Task<string> GenerateThumbnailAsync(IFormFile file)
        {
            // Only generate thumbnails for images
            if (!file.ContentType.StartsWith("image/"))
                return string.Empty;

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            // Load the image using ImageSharp
            using var image = await Image.LoadAsync(memoryStream);

            // Resize the image to a maximum width of 200px while keeping the aspect ratio
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(200, 0),
                Mode = ResizeMode.Max
            }));

            // We will return the thumbnail as a Base64 string so it can be saved directly to the DB or sent to the frontend
            return image.ToBase64String(SixLabors.ImageSharp.Formats.Jpeg.JpegFormat.Instance);
        }
    }
}
