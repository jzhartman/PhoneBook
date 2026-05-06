using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.Application.DeleteContact;

public class DeleteContactHandler
{
    private readonly IContactRepository _repo;

    public DeleteContactHandler(IContactRepository repo)
    {
        _repo = repo;
    }

    public async Task<Result> HandleAsync(ContactResponse contact)
    {
        var result = await _repo.DeleteAsync(
                new Contact
                {
                    ContactId = contact.ContactId,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    Email = contact.Email
                });

        if (result is null)
            return Result.Failure(Errors.DeleteResponseNull);
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Success();
    }
}
