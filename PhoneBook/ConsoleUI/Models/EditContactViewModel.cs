using PhoneBook.Application.Contacts.DTOs;

namespace PhoneBook.ConsoleUI.Models;

internal class EditContactViewModel
{
    public bool ChangedFirstName { get; set; } = false;
    public bool ChangedLastName { get; set; } = false;
    public bool ChangedPhoneNumber { get; set; } = false;
    public bool ChangedEmail { get; set; } = false;

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public int CategoryId { get; set; }

    public EditContactViewModel(ContactResponse originalContact)
    {
        Id = originalContact.ContactId;
        FirstName = originalContact.FirstName;
        LastName = originalContact.LastName;
        PhoneNumber = originalContact.PhoneNumber;
        Email = originalContact.Email;
        CategoryId = originalContact.CategoryId;
    }
}
