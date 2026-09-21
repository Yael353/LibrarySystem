using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;
        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Loans.FindAsync(new object[] { id }, cancellationToken);
        }
        public async Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Loans.ToListAsync(cancellationToken);
        }
        //Specialmetod 1: Aktiva lån för en medlem
        public async Task<List<Loan>> GetActiveLoansByMemberIdAsync(Guid memberId, CancellationToken cancellationToken = default)
        {
            return await _context.Loans
                .Where(l => l.MemberId == memberId && l.ReturnedDate == null)
                .ToListAsync(cancellationToken);
        }
        // Specialmetod 2: Aktivt lån för en bok
        public async Task<Loan?> GetActiveLoanByBookIdAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            return await _context.Loans
                .FirstOrDefaultAsync(l => l.BookId == bookId && l.ReturnedDate == null, cancellationToken);
        }
        public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
        {
            await _context.Loans.AddAsync(loan, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Loan loan, CancellationToken cancellationToken = default)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
