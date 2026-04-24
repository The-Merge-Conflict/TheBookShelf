using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.ValueObjects
{
    public record FileSize
    {
        public long Bytes { get; init; }

        private FileSize(long bytes)
        {
            Bytes = bytes;
        }

        public static FileSize Create(long bytes)
        {
            if (bytes < 0)
                throw new ArgumentException("File size cannot be negative.");

            return new FileSize(bytes);
        }

        public double ToKilobytes() => Bytes / 1024.0;
        public double ToMegabytes() => Bytes / (1024.0 * 1024.0);
    }
}
