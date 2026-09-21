using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibrarySystem.Infrastructure.Persistence;

public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
{
    public LibraryDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();

        // Connection string för design-time (migrationer)
        optionsBuilder.UseSqlite("Data Source=library.db");

        return new LibraryDbContext(optionsBuilder.Options);
    }
}