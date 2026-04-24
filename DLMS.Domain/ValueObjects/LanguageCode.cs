using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DLMS.Domain.ValueObjects
{
    public record LanguageCode
    {
        public string Code { get; init; }

        private LanguageCode(string code)
        {
            Code = code;
        }

        public static LanguageCode Create(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Language code cannot be empty.");

            code = code.Trim().ToLowerInvariant();

            if (!Regex.IsMatch(code, @"^[a-z]{2}$"))
                throw new ArgumentException($"'{code}' is not a valid 2-letter ISO-639-1 language code.");

            return new LanguageCode(code);
        }
    }
}
