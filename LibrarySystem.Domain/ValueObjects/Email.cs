using LibrarySystem.Domain.Common;
using System.Net.Mail;

namespace LibrarySystem.Domain.ValueObjects;

// Value objects ska alltid vara sealed, det förhindrar arv som kan kringgå validering.
public sealed class Email : ValueObject
{
    public string Value { get; }

    private Email() { }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("E-post får inte vara tom", nameof(value));

        value = value.Trim();

        try
        {
            // MailAddress validerar formatet.
            // Ogiltigt format → FormatException.
            var addr = new MailAddress(value);

            // MailAddress kan normalisera värdet, säkerställ att det matchar input.
            if (addr.Address != value)
                throw new ArgumentException($"Ogiltig e-postadress: {value}", nameof(value));

            Value = addr.Address.ToLowerInvariant();
        }
        catch (FormatException)
        {
            // Value objects vägrar existera i ogiltigt tillstånd, kastar exception.
            throw new ArgumentException($"Ogiltig e-postadress: {value}", nameof(value));
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}