using LibrarySystem.Application.DTOs;


namespace LibrarySystem.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<BookDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(string title, string author, string isbn, string? coverUrl, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, string title, string author, string isbn, string? coverUrl, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
