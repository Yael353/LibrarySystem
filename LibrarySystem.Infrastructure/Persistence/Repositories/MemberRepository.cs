using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Persistence.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext _context;
        public MemberRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Members.FindAsync(new object [] { id }, cancellationToken);
        }
        public async Task<List<Member>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Members.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Member member, CancellationToken cancellationToken = default)
        {
            await _context.Members.AddAsync(member, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(Member member, CancellationToken cancellationToken = default)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteAsync(Member member, CancellationToken cancellationToken = default)
        {
            _context.Members.Remove(member);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
