using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Application.Interfaces.Services
{
    public interface IMemberService
    {
        Task<MemberDto?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);
        Task<List<MemberDto>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<Guid> CreateAsync(
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            CancellationToken cancellationToken = default);
        Task UpdateAsync(
            Guid id,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            CancellationToken cancellationToken = default);
        Task DeactivateAsync(
            Guid id,
            CancellationToken cancellationToken = default);
        Task ActivateAsync(
            Guid id,
            CancellationToken cancellationToken = default);
    }
}
