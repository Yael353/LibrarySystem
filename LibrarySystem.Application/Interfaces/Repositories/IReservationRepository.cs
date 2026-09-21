using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Interfaces.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // affärslogik
    Task<List<Reservation>> GetPendingByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task<Reservation?> GetActiveByMemberAndBookAsync(Guid memberId, Guid bookId, CancellationToken cancellationToken = default);

    Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default);
    Task UpdateAsync(Reservation reservation, CancellationToken cancellationToken = default);
}