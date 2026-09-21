using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.ToTable("Reservations");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.BookId)
            .IsRequired();

        builder.Property(r => r.MemberId)
            .IsRequired();

        builder.Property(r => r.ReservedDate)
            .IsRequired();

        // Enum sparas som sträng ("Pending", "Fulfilled", "Cancelled")
        builder.Property(r => r.Status)
            .HasConversion<string>()
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(r => r.FulfilledDate);
    }
}