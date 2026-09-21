using LibrarySystem.Domain.Common;
using System;                       
using System.Collections.Generic;


namespace LibrarySystem.Domain.ValueObjects
{
    public sealed class PersonName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

        private PersonName() { }
        public PersonName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Förnamn måste vara ifyllt", nameof(firstName));
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Efternamn måste vara ifyllt", nameof(lastName));

            FirstName = firstName.Trim();
            LastName = lastName.Trim();

        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";

    }
}
