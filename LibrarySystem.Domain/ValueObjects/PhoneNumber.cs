using LibrarySystem.Domain.Common;


namespace LibrarySystem.Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; }

        private PhoneNumber() { }
        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Telefonnummer måste vara ifyllt", nameof(value));

            // 1. Ta bort mellanslag, bindestreck och parenteser
            var cleaned = value
                .Replace(" ", "")
                .Replace("-", "")
                .Replace("(", "")
                .Replace(")", "")
                .Trim();

            // 2. Kontrollera att det bara innehåller siffror och eventuellt + i början
            var digitsPart = cleaned.StartsWith("+") ? cleaned[1..] : cleaned;

            if (!digitsPart.All(char.IsDigit))
                throw new ArgumentException("Telefonnummer får bara innehålla siffror, +, -, mellanslag och parenteser", nameof(value));

            // 3. Kontrollera längd (7-15 siffror enligt E.164)
            if (digitsPart.Length < 7 || digitsPart.Length > 15)
                throw new ArgumentException("Telefonnummer måste vara mellan 7 och 15 siffror", nameof(value));

            Value = cleaned;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

       public override string ToString() => Value;

    }



}
