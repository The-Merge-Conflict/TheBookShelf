using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.ValueObjects
{
    public record FileDimensions
    {
        public int Width { get; init; }
        public int Height { get; init; }

        private FileDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static FileDimensions Create(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Width and Height must be greater than zero.");

            return new FileDimensions(width, height);
        }

        public double AspectRatio => (double)Width / Height;
        public bool IsLandscape => Width > Height;
        public bool IsPortrait => Height > Width;
    }
}
