using LibrarySystem.Domain.Common;
using LibrarySystem.Domain.ValueObjects;

namespace LibrarySystem.Domain.Entities
{
    public class Book : Entity
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public ISBN ISBN { get; private set; }
        public bool IsAvailable { get; private set; } = true;
        public LoanPeriod LoanPeriod { get; private set; } = LoanPeriod.Standard();
        public string? CoverUrl { get; private set; }

        private Book() { }
        private Book(string title, string author, ISBN isbn, string? coverUrl) : base()
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Titel får inte vara tom", nameof(title));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Författare får inte vara tom", nameof(author));

            Title = title;
            Author = author;
            ISBN = isbn;
            CoverUrl = coverUrl;
        }
        public static Book Create(string title, string author, ISBN isbn, string? coverUrl = null)
        {
            return new Book(title, author, isbn, coverUrl);
        }
        public void Update(string title, string author, ISBN isbn, string? coverUrl)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Titel får inte vara tom", nameof(title));
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Författare får inte vara tom", nameof(author));

            Title = title;
            Author = author;
            ISBN = isbn;
            CoverUrl = coverUrl;
        }
        public void MarkAsBorrowed()
        {
            if (!IsAvailable)
                throw new InvalidOperationException("Boken är för närvarande utlånad");

            IsAvailable = false;
        }
        public void MarkAsReturned()
        {
            if (IsAvailable)
                throw new InvalidOperationException("Boken är inte utlånad");

            IsAvailable = true;
        }

        public string GetDisplayInfo()
        {
            var status = IsAvailable ? "tillgänglig" : "utlånad";
            return $"{Title} av {Author} (ISBN: {ISBN}) - {status}";
        }
        
     
    }
}