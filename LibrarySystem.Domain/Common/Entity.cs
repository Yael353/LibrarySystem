namespace LibrarySystem.Domain.Common
{
    /// Basklass för alla entiteter i domänen.
    /// Alla entiteter har ett unikt identifierande attribut, här är det ID:et.
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        // En konstruktor som skapar ett nytt objekt med ett unikt ID.
        protected Entity()
        {
            //Guid kan genereras utan databasen. Viktigt i DDD – du kan skapa objekt utan att först fråga databasen.
            Id = Guid.NewGuid();
        }
        // Konstraktor två som återskapar något som redan finns, och tar emot ID:et på det redan befintliga objektet som parameter. Annars riskerar vi att sätta nya id:en för befintliga böcker = KaozZzzZ
        protected Entity(Guid id)
        {
            Id = id;
        }

        // Två entiteter anses vara lika om deras ID är lika (Och samma typ).
        // Vi går inte på titel exempelvis då flera böcker kan ha flera
        // likadana titlar utan att vara samma bok.
        public override bool Equals(object obj)
        {
            if (obj is not Entity other)
                return false;
           

            if(ReferenceEquals(this, other)) 
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Entity? left, Entity? right)
        {
            return !Equals(left, right);
        }
    }
}
