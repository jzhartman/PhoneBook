using PhoneBook.Domain.Entities;

namespace PhoneBook.Application.Contacts.DTOs;

internal static class ContactResponseHelper
{
    internal static List<ContactResponse> MapToContactResponse(List<Contact> contacts)
    {
        var contactResponseList = new List<ContactResponse>();

        foreach (var contact in contacts)
        {
            contactResponseList.Add(new ContactResponse
            (
                contact.Id,
                contact.FirstName,
                contact.LastName,
                contact.PhoneNumber,
                contact.Email,
                contact.CategoryId
            ));
        }

        return contactResponseList;
    }
}
