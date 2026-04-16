using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Application.EditContact;

public class EditContactHandler
{
    private readonly IContactRepository _repo;

    public EditContactHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(ContactResponse contact)
    {
        await _repo.UpdateAsync(new Contact
        {
            ContactId = contact.ContactId,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            PhoneNumber = contact.PhoneNumber,
            Email = contact.Email
        });
    }
}
