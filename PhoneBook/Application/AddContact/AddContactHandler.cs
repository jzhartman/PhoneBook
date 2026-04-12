using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Application.AddContact;

internal class AddContactHandler
{
    private readonly IContactRepository _repo;

    public AddContactHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(AddContactRequest contact)
    {
        await _repo.AddAsync(
            new Contact
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                PhoneNumber = contact.PhoneNumber
            });
    }
}
