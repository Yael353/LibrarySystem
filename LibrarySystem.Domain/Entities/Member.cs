using LibrarySystem.Domain.Common;
using LibrarySystem.Domain.ValueObjects;


namespace LibrarySystem.Domain.Entities
{
    public class Member : Entity
    {
        public PersonName Name { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public bool IsActive { get; private set; } = true;

        private Member() { }

        private Member(PersonName name, PhoneNumber phoneNumber, Email email) : base()
        {
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public static Member Create(PersonName name, PhoneNumber phoneNumber, Email email)
        {
            return new Member(name, phoneNumber, email);
        }

        public void Update(PersonName name, Email email, PhoneNumber phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
        public void Activate()
        {
            IsActive = true;

        }

    }
}
