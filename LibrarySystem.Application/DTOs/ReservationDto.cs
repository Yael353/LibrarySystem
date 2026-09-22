namespace LibrarySystem.Application.DTOs;

public class ReservationDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid MemberId { get; set; }
    public DateTime ReservedDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime? FulfilledDate { get; set; }
}