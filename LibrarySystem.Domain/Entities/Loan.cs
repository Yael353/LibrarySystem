using LibrarySystem.Domain.Common;
using LibrarySystem.Domain.ValueObjects;


namespace LibrarySystem.Domain.Entities
{
    public class Loan : Entity
    {
        public Guid BookId { get; private set; }
        public Guid MemberId { get; private set; }

        public DateTime LoanDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? ReturnedDate { get; private set; }

        private Loan() { }

        private Loan(Guid bookId, Guid memberId, LoanPeriod loanPeriod) : base()
        {
            BookId = bookId;
            MemberId = memberId;
            LoanDate = DateTime.UtcNow;
            DueDate = loanPeriod.GetDueDate(DateTime.UtcNow);
            ReturnedDate = null;
            
        }
        public static Loan Create(Guid bookId, Guid memberId, LoanPeriod loanPeriod)
        {
            return new Loan(bookId, memberId, loanPeriod);
        }

        public void MarkAsReturned()
        {
            if (ReturnedDate != null)
                throw new InvalidOperationException("Boken är redan återlämnad");

            ReturnedDate = DateTime.UtcNow;
        }

        public bool IsOverdue()
        {
            if (ReturnedDate == null)
                return DueDate < DateTime.UtcNow;

            return ReturnedDate > DueDate;
        }

    }

   
}
