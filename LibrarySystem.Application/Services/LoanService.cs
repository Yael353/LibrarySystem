using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IBookRepository _bookRepository;

    public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository)
    {
        _loanRepository = loanRepository;
        _bookRepository = bookRepository;
    }

    public async Task<LoanDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var loan = await _loanRepository.GetByIdAsync(id, cancellationToken);
        if (loan == null) return null;

        return MapToDto(loan);
    }

    public async Task<List<LoanDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var loans = await _loanRepository.GetAllAsync(cancellationToken);
        return loans.Select(MapToDto).ToList();
    }

    public async Task<List<LoanDto>> GetActiveLoansByMemberIdAsync(Guid memberId, CancellationToken cancellationToken = default)
    {
        var loans = await _loanRepository.GetActiveLoansByMemberIdAsync(memberId, cancellationToken);
        return loans.Select(MapToDto).ToList();
    }

    public async Task<Guid> CreateAsync(Guid bookId, Guid memberId, CancellationToken cancellationToken = default)
    {
        // 1. Hämta boken (för att få LoanPeriod)
        var book = await _bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book == null)
            throw new InvalidOperationException($"Bok med id {bookId} hittades inte");

        // 2. Markera boken som utlånad (affärsregel)
        book.MarkAsBorrowed();
        await _bookRepository.UpdateAsync(book, cancellationToken);

        // 3. Skapa lånet
        var loan = Loan.Create(bookId, memberId, book.LoanPeriod);
        await _loanRepository.AddAsync(loan, cancellationToken);

        return loan.Id;
    }

    public async Task ReturnAsync(Guid loanId, CancellationToken cancellationToken = default)
    {
        // 1. Hämta lånet
        var loan = await _loanRepository.GetByIdAsync(loanId, cancellationToken);
        if (loan == null)
            throw new InvalidOperationException($"Lån med id {loanId} hittades inte");

        // 2. Markera lånet som återlämnat
        loan.MarkAsReturned();
        await _loanRepository.UpdateAsync(loan, cancellationToken);

        // 3. Markera boken som tillgänglig
        var book = await _bookRepository.GetByIdAsync(loan.BookId, cancellationToken);
        if (book != null)
        {
            book.MarkAsReturned();
            await _bookRepository.UpdateAsync(book, cancellationToken);
        }
    }

    private static LoanDto MapToDto(Loan loan)
    {
        return new LoanDto
        {
            Id = loan.Id,
            BookId = loan.BookId,
            MemberId = loan.MemberId,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnedDate = loan.ReturnedDate,
            IsOverdue = loan.IsOverdue()
        };
    }
}