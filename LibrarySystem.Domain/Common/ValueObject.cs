namespace LibrarySystem.Domain.Common
{
    /// <summary>
    /// Bas klass för värdeobjekt i domänen.
    /// Ett Value Object är ett litet, oföränderligt objekt som beskriver ett värde – inte en identitet.
    /// Värdeobjekt är oföränderliga och jämförs baserat på sina egenskaper.
    /// Säkerställer att ogiltiga tillstånd inte är möjliga, exempelvis "intee-en-Email" (fel) vs "test@test.se (rätt)
    /// </summary>
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();
        
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;


            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            var other = (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode() 
        {
            return GetEqualityComponents().Aggregate(new HashCode(), (hash, component) =>
            {
                hash.Add(component);
                return hash;
            }).ToHashCode();
        }

        public static bool operator ==(ValueObject? left, ValueObject? right) 
        {
            return Equals(left, right);
        }
        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !Equals(left, right);
        }
    }
}
