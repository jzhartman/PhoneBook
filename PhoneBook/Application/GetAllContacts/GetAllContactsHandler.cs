using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;

namespace PhoneBook.Application.GetAllContacts;

public class GetAllContactsHandler
{
    private readonly IContactRepository _repo;

    public GetAllContactsHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ContactResponse>> HandleAsync()
    {
        var contacts = await _repo.GetAllAsync();

        return MapToContactResponse(contacts);
    }

    private List<ContactResponse> MapToContactResponse(List<Contact> contacts)
    {
        var contactResponseList = new List<ContactResponse>();

        foreach (var contact in contacts)
        {
            contactResponseList.Add(new ContactResponse
            (
                contact.ContactId,
                contact.FirstName,
                contact.LastName,
                contact.PhoneNumber,
                contact.Email
            ));
        }

        return contactResponseList;
    }
}
