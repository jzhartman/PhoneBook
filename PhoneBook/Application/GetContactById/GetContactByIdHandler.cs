using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;

namespace PhoneBook.Application.GetById;

internal class GetContactByIdHandler
{
    private readonly IContactRepository _repo;

    public GetContactByIdHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task<ContactResponse> HandleAsync(int contactId)
    {
        var contact = await _repo.GetByIdAsync(contactId);

        return (new ContactResponse
            (
                contact.ContactId,
                contact.FirstName,
                contact.LastName,
                contact.PhoneNumber,
                contact.Email
            ));
    }
}
