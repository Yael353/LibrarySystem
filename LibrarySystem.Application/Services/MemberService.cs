using LibrarySystem.Application.DTOs;
using LibrarySystem.Application.Interfaces.Repositories;
using LibrarySystem.Application.Interfaces.Services;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.ValueObjects;


namespace LibrarySystem.Application.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }


        public async Task<MemberDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
            if (member == null) return null;

            return MapToDto(member);
        }
        public async Task<List<MemberDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var books = await _memberRepository.GetAllAsync(cancellationToken);

            return books.Select(MapToDto).ToList();
        }
        public async Task<Guid> CreateAsync(string firstName, string lastName, string email, string phoneNumber, CancellationToken cancellationToken = default)
        {
            var name = new PersonName(firstName, lastName);
            var emailValue = new Email(email);
            var phoneValue = new PhoneNumber(phoneNumber);

            var member = Member.Create(name, phoneValue, emailValue);
            
            await _memberRepository.AddAsync(member, cancellationToken);

            return member.Id;
        }


        public async Task UpdateAsync(Guid id, string firstName, string lastName, string email, string phoneNumber, CancellationToken cancellationToken = default)
        {
            var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
            if (member == null) throw new InvalidOperationException($"Medlem med id: {id} kunde inte hittas");

            var name = new PersonName(firstName, lastName);
            var emailValue = new Email(email);
            var phoneValue = new PhoneNumber(phoneNumber);

            member.Update(name, emailValue, phoneValue);

            await _memberRepository.UpdateAsync(member, cancellationToken);
        }
        public async Task DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
            if (member == null) throw new InvalidOperationException($"Medlem med id: {id} kunde inte hittas");

            member.Deactivate();
            await _memberRepository.UpdateAsync(member, cancellationToken);
        }
        public async Task ActivateAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var member = await _memberRepository.GetByIdAsync(id, cancellationToken);
            if (member == null) throw new InvalidOperationException($"Medlem med id: {id} kunde inte hittas");

            member.Activate();
            await _memberRepository.UpdateAsync(member, cancellationToken);
        }

        private static MemberDto MapToDto(Member member)
        {
            return new MemberDto
            {
                Id = member.Id,
                FirstName = member.Name.FirstName,
                LastName = member.Name.LastName,
                Email = member.Email.Value,
                PhoneNumber = member.PhoneNumber.Value,

            };
        }
    }
}
