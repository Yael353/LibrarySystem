using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Infrastructure.Persistence.Configurations;

public class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.IsActive)
            .IsRequired();

        // PersonName – owned entity (två properties: FirstName, LastName)
        builder.OwnsOne(m => m.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(100)
                .IsRequired();

            name.Property(n => n.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(100)
                .IsRequired();
        });

        // Email – value converter (en property: Value)
        builder.Property(m => m.Email)
            .HasConversion(
                email => email.Value,
                value => new Email(value))
            .HasColumnName("Email")
            .HasMaxLength(200)
            .IsRequired();

        // PhoneNumber – value converter (en property: Value)
        builder.Property(m => m.PhoneNumber)
            .HasConversion(
                phone => phone.Value,
                value => new PhoneNumber(value))
            .HasColumnName("PhoneNumber")
            .HasMaxLength(20)
            .IsRequired();
    }
}