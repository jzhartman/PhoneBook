using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.Application.AddContact;

public class AddContactHandler
{
    private readonly IContactRepository _repo;

    public AddContactHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> HandleAsync(AddContactRequest contact)
    {
        var result = await _repo.AddAsync(
            new Contact
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email
            });

        if (result is null)
            return Result.Failure(Errors.AddResponseNull);
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Success();
    }
}
