using LibrarySystem.Application.DTOs;

namespace LibrarySystem.Application.Interfaces.Services;

public interface ILoanService
{
    Task<LoanDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LoanDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<List<LoanDto>> GetActiveLoansByMemberIdAsync(Guid memberId, CancellationToken cancellationToken = default);
    Task<Guid> CreateAsync(Guid bookId, Guid memberId, CancellationToken cancellationToken = default);
    Task ReturnAsync(Guid loanId, CancellationToken cancellationToken = default);
}