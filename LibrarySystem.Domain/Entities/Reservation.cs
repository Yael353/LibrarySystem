using LibrarySystem.Domain.Common;
using LibrarySystem.Domain.Enums;

namespace LibrarySystem.Domain.Entities
{
    public class Reservation : Entity
    {
        public Guid BookId { get; private set; }
        public Guid MemberId { get; private set; }
        public DateTime ReservedDate { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime? FulfilledDate { get; private set; }

        private Reservation() { }
        private Reservation(Guid bookId, Guid memberId) : base()
        {
            if (bookId == Guid.Empty)
                throw new ArgumentException("BookId får inte vara tomt", nameof(bookId));
            if (memberId == Guid.Empty)
                throw new ArgumentException("MemberId får inte vara tomt", nameof(memberId));

            BookId = bookId;
            MemberId = memberId;
            ReservedDate = DateTime.UtcNow;
            Status = ReservationStatus.Pending;
            FulfilledDate = null;
        }


        public static Reservation Create(Guid bookId, Guid memberId)
        {
            return new Reservation(bookId, memberId);
        }


        public void Fulfill()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException("Endast väntande reservationer kan uppfyllas");

            Status = ReservationStatus.Fulfilled;
            FulfilledDate = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status != ReservationStatus.Pending)
                throw new InvalidOperationException("Endast väntande reservationer kan avbrytas");

            Status = ReservationStatus.Cancelled;
        }
    }
}