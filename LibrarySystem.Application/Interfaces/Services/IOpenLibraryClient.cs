using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Application.Interfaces.Services;

public interface IOpenLibraryClient
{
    Task<List<OpenLibraryBookDto>> SearchAsync(string query, CancellationToken cancellationToken = default);
    Task<OpenLibraryBookDto?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
}