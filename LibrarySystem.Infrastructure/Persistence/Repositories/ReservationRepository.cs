using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace LibrarySystem.Infrastructure.Persistence.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly LibraryDbContext _context;

        public ReservationRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations.FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<List<Reservation>> GetPendingByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .Where(r => r.BookId == bookId && r.Status == ReservationStatus.Pending)
                .OrderBy(r => r.ReservedDate)
                .ToListAsync(cancellationToken);
        }
        public async Task<Reservation?> GetActiveByMemberAndBookAsync(Guid memberId, Guid bookId, CancellationToken cancellationToken = default)
        {
            return await _context.Reservations
                .FirstOrDefaultAsync(r => r.MemberId == memberId
                                       && r.BookId == bookId
                                       && r.Status == ReservationStatus.Pending, cancellationToken);
        }
        public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default)
        {
            await _context.Reservations.AddAsync(reservation, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
