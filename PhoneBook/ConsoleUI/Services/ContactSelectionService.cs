using PhoneBook.Application.DTOs;
using PhoneBook.Application.GetAllContacts;
using PhoneBook.ConsoleUI.Views;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

namespace PhoneBook.ConsoleUI.Services;

internal class ContactSelectionService
{
    private readonly GetAllContactsHandler _getAllContactsHandler;
    private readonly ContactSelectionView _contactSelectionView;

    public ContactSelectionService(GetAllContactsHandler getAllContactsHandler, ContactSelectionView contactSelectionView)
    {
        _getAllContactsHandler = getAllContactsHandler;
        _contactSelectionView = contactSelectionView;
    }

    public async Task<Result<ContactResponse>> RunAsync()
    {
        var result = await _getAllContactsHandler.HandleAsync();

        if (result.IsFailure)
            return Result<ContactResponse>.Failure(result.Errors);

        if (result.Value is null)
            return Result<ContactResponse>.Failure(new[] { Errors.GetResponseNull });

        return Result<ContactResponse>.Success(_contactSelectionView.Render(result.Value.ToArray()));
    }
}
