using LibrarySystem.Domain.Entities;

namespace LibrarySystem.Application.Interfaces.Repositories;

public interface ILoanRepository
{
    Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken = default);

    // Affärslogik
    Task<List<Loan>> GetActiveLoansByMemberIdAsync(Guid memberId, CancellationToken cancellationToken = default);
    Task<Loan?> GetActiveLoanByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default);

    Task AddAsync(Loan loan, CancellationToken cancellationToken = default);
    Task UpdateAsync(Loan loan, CancellationToken cancellationToken = default);
}