using LibrarySystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.Application.Interfaces.Services
{
    public interface IBookService
    {
        Task<BookDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<BookDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Guid> CreateAsync(string title, string author, string isbn, CancellationToken cancellationToken = default);
        Task UpdateAsync(Guid id, string title, string author, string isbn, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
