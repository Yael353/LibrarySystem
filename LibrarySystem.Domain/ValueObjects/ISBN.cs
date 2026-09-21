using LibrarySystem.Domain.Common;
using System;                      
using System.Collections.Generic;  
using System.Linq;

namespace LibrarySystem.Domain.ValueObjects
{
    public sealed class ISBN : ValueObject
    {
        public string Value { get; }

        private ISBN() { }

        public ISBN(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ISBN måste vara ifyllt", nameof(value));

            var cleaned = value
               .Replace(" ", "")
               .Replace("-", "")
               .Trim();

            if (!cleaned.All(char.IsDigit))
                throw new ArgumentException("ISBN får bara innehålla siffror", nameof(value));
            
            if (cleaned.Length != 10 && cleaned.Length != 13)
                throw new ArgumentException("ISBN kan bara vara 10 eller 13 siffror", nameof(value));

            Value = cleaned;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
