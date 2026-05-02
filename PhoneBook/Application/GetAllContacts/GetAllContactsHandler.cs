using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;

namespace PhoneBook.Application.GetAllContacts;

public class GetAllContactsHandler
{
    private readonly IContactRepository _repo;

    public GetAllContactsHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result<List<ContactResponse>>> HandleAsync()
    {
        var result = await _repo.GetAllAsync();

        if (result == null || result.Value == null)
            return Result<List<ContactResponse>>.Success(new List<ContactResponse>());

        if (result.IsFailure)
            return Result<List<ContactResponse>>.Failure(result.Errors);

        return Result<List<ContactResponse>>.Success(MapToContactResponse(result.Value));
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
