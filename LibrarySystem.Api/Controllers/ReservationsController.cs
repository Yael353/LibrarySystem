using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    // GET: api/reservations/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var reservation = await _reservationService.GetByIdAsync(id, cancellationToken);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }

    // GET: api/reservations/book/{bookId}
    [HttpGet("book/{bookId}")]
    public async Task<ActionResult<List<ReservationDto>>> GetPendingByBook(
        Guid bookId,
        CancellationToken cancellationToken)
    {
        var reservations = await _reservationService.GetPendingByBookIdAsync(bookId, cancellationToken);
        return Ok(reservations);
    }

    // POST: api/reservations
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateReservationRequest request,
        CancellationToken cancellationToken)
    {
        var id = await _reservationService.CreateAsync(
            request.BookId,
            request.MemberId,
            cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    // POST: api/reservations/{id}/fulfill
    [HttpPost("{id}/fulfill")]
    public async Task<IActionResult> Fulfill(Guid id, CancellationToken cancellationToken)
    {
        await _reservationService.FulfillAsync(id, cancellationToken);
        return NoContent();
    }

    // POST: api/reservations/{id}/cancel
    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        await _reservationService.CancelAsync(id, cancellationToken);
        return NoContent();
    }
}

// Request-modeller
public record CreateReservationRequest(Guid BookId, Guid MemberId);