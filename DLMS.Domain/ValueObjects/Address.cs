using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLMS.Domain.ValueObjects
{
    public record Address(string Street, string City, string State, string ZipCode, string Country)
    {
        public Address() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty) { }

        public static Address Create(string street, string city, string state, string zipCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("Street cannot be empty");

            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("Street cannot be empty");

            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentException("Country cannot be empty");

            return new Address(street, city, state, zipCode, country);
        }
    }
}
