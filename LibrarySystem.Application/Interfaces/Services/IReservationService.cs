using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Application.Interfaces.Services;

public interface IReservationService
{
    Task<ReservationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ReservationDto>> GetPendingByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(Guid bookId, Guid memberId, CancellationToken cancellationToken = default);
    Task FulfillAsync(Guid reservationId, CancellationToken cancellationToken = default);
    Task CancelAsync(Guid reservationId, CancellationToken cancellationToken = default);
}