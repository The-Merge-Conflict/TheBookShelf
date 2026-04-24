using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.ValueObjects
{
    public record MimeType
    {
        public string Value { get; init; }
        private MimeType(string value)
        {
            Value = value;
        }

        public static MimeType Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("MIME type cannot be empty.");

            if (!value.Contains('/'))
                throw new ArgumentException($"'{value}' is not a valid MIME type format.");

            return new MimeType(value.ToLowerInvariant());
        }
    }
}
