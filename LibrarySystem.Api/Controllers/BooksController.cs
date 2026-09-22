using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController( IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetAll(CancellationToken cancellationToken)
        {
            var books = await _bookService.GetAllAsync(cancellationToken);

            return Ok(books);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var book = await _bookService.GetByIdAsync(id, cancellationToken);
            if (book == null) return NotFound();
            return Ok(book);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
       [FromBody] CreateBookRequest request,
       CancellationToken cancellationToken)
        {
            var id = await _bookService.CreateAsync(
                request.Title,
                request.Author,
                request.ISBN,
                cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateBookRequest request,
            CancellationToken cancellationToken)
        {
            await _bookService.UpdateAsync(id, request.Title, request.Author, request.ISBN, cancellationToken);
            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _bookService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }

    
}

    public record CreateBookRequest(string Title, string Author, string ISBN);
    public record UpdateBookRequest(string Title, string Author, string ISBN);
