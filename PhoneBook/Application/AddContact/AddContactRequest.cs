namespace PhoneBook.Application.AddContact;

public record AddContactRequest
(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email
);