using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<ReservationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id, cancellationToken);
        if (reservation == null) return null;

        return MapToDto(reservation);
    }

    public async Task<List<ReservationDto>> GetPendingByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        var reservations = await _reservationRepository.GetPendingByBookIdAsync(bookId, cancellationToken);
        return reservations.Select(MapToDto).ToList();
    }

    public async Task<Guid> CreateAsync(Guid bookId, Guid memberId, CancellationToken cancellationToken = default)
    {
        // Kolla om medlemmen redan har en aktiv reservation för boken
        var existing = await _reservationRepository.GetActiveByMemberAndBookAsync(memberId, bookId, cancellationToken);
        if (existing != null)
            throw new InvalidOperationException("Medlemmen har redan en aktiv reservation för denna bok");

        var reservation = Reservation.Create(bookId, memberId);
        await _reservationRepository.AddAsync(reservation, cancellationToken);

        return reservation.Id;
    }

    public async Task FulfillAsync(Guid reservationId, CancellationToken cancellationToken = default)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId, cancellationToken);
        if (reservation == null)
            throw new InvalidOperationException($"Reservation med id {reservationId} hittades inte");

        reservation.Fulfill();
        await _reservationRepository.UpdateAsync(reservation, cancellationToken);
    }

    public async Task CancelAsync(Guid reservationId, CancellationToken cancellationToken = default)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId, cancellationToken);
        if (reservation == null)
            throw new InvalidOperationException($"Reservation med id {reservationId} hittades inte");

        reservation.Cancel();
        await _reservationRepository.UpdateAsync(reservation, cancellationToken);
    }

    private static ReservationDto MapToDto(Reservation reservation)
    {
        return new ReservationDto
        {
            Id = reservation.Id,
            BookId = reservation.BookId,
            MemberId = reservation.MemberId,
            ReservedDate = reservation.ReservedDate,
            Status = reservation.Status.ToString(),
            FulfilledDate = reservation.FulfilledDate
        };
    }
}