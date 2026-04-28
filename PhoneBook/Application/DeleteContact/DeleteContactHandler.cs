using PhoneBook.Application.DTOs;
using PhoneBook.Application.Interfaces;
using PhoneBook.Domain.Entities;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.Application.DeleteContact;

internal class DeleteContactHandler
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
                    Email = contact.Email,
                    PhoneNumber = contact.PhoneNumber
                });

        if (result == null)
            return Result.Failure(Errors.GenericNull);
        if (result.IsFailure)
            return Result.Failure(result.Errors);

        return Result.Success();
    }
}
