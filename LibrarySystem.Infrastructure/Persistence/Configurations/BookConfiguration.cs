using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(b => b.IsAvailable)
            .IsRequired();

        builder.Property(b => b.ISBN)
            .HasConversion(
                isbn => isbn.Value,
                value => new ISBN(value))
            .HasColumnName("ISBN")
            .HasMaxLength(13)
            .IsRequired();

        builder.OwnsOne(b => b.LoanPeriod, period =>
        {
            period.Property(p => p.Days)
                .HasColumnName("LoanPeriodDays")
                .IsRequired();
        });
    }
}