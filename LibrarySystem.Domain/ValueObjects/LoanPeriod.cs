using LibrarySystem.Domain.Common;
using System;
using System.Collections.Generic;

namespace LibrarySystem.Domain.ValueObjects
{
    public sealed class LoanPeriod : ValueObject
    {
        public int Days { get; }
        private LoanPeriod() { }
        public LoanPeriod(int days)
        {
            if (days < 1 || days > 90)
                throw new ArgumentException("Lånperioden måste vara mellan 1 och 90 dagar", nameof(days));

            Days = days;
        }

        public DateTime GetDueDate(DateTime fromDate)
        {
            return fromDate.AddDays(Days);
        }

        public static LoanPeriod Standard() => new LoanPeriod(30);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Days;
        }

        public override string ToString() => $"{Days} dagar";
    }
}