using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ValueObjects;

namespace LibrarySystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            if (book == null) return null;

            return MapToDto(book);
        }

        public async Task<List<BookDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var books = await _bookRepository.GetAllAsync(cancellationToken);
            return books.Select(MapToDto).ToList();
        }
        public async Task<Guid> CreateAsync(string title, string author, string isbn, string? coverUrl, CancellationToken cancellationToken = default)
        {
            var isbnValue = new ISBN(isbn);
            var book = Book.Create(title, author, isbnValue, coverUrl);

            await _bookRepository.AddAsync(book, cancellationToken);
            return book.Id;
        }

        public async Task UpdateAsync(Guid id, string title, string author, string isbn, string? coverUrl, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            if (book == null)
                throw new InvalidOperationException($"Bok med id {id} hittades inte");

            var isbnValue = new ISBN(isbn);
            book.Update(title, author, isbnValue, coverUrl);

            await _bookRepository.UpdateAsync(book, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var book = await _bookRepository.GetByIdAsync(id, cancellationToken);
            if (book == null)
                throw new InvalidOperationException($"Bok med id {id} hittades inte");

            await _bookRepository.DeleteAsync(book, cancellationToken);
        }

        private static BookDto MapToDto(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN.Value,
                IsAvailable = book.IsAvailable,
                CoverUrl = book.CoverUrl
            };
        }
    }
}
