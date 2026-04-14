using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Application.DeleteContact;

internal class DeleteContactHandler
{
    private readonly IContactRepository _repo;

    public DeleteContactHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task HandleAsync(ContactResponse contact)
    {
        await _repo.DeleteAsync(
                new Contact
                {
                    ContactId = contact.ContactId,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber
                });
    }
}
