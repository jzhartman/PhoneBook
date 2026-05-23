using PhoneBook.Application.Categories.DTOs;
using PhoneBook.Application.Contacts.DTOs;
using PhoneBook.Application.Contacts.GetAllContacts;
using PhoneBook.Application.Contacts.GetContactsByCategoryId;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Views;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;


namespace PhoneBook.ConsoleUI.Services.Contacts;

internal class ContactSelectionService
{
    private readonly GetAllContactsHandler _getAllContactsHandler;
    private readonly GetAllContactsByCategoryIdHandler _getAllContactsByCategoryIdHandler;
    private readonly ContactSelectionView _contactSelectionView;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public ContactSelectionService(GetAllContactsHandler getAllContactsHandler, ContactSelectionView contactSelectionView,
                                    GetAllContactsByCategoryIdHandler getAllContactsByCategoryIdHandler,
                                    Messages messages, UserInput userInput)
    {
        _getAllContactsHandler = getAllContactsHandler;
        _getAllContactsByCategoryIdHandler = getAllContactsByCategoryIdHandler;
        _contactSelectionView = contactSelectionView;
        _messages = messages;
        _userInput = userInput;
    }

    public async Task<ContactResponse?> RunAsync(CategoryResponse category)
    {
        Result<List<ContactResponse>> result;

        if (category.Id == -1 && category.Name == "ALL")
            result = await _getAllContactsHandler.HandleAsync();
        else
            result = await _getAllContactsByCategoryIdHandler.HandleAsync(category);


        if (result.IsFailure || result.Value is null)
        {
            _messages.ErrorMessage(result.Errors);
            _userInput.PressAnyKeyToContinue();
            return null;
        }

        if (result.Value.Count == 0)
        {
            _messages.ErrorMessage(new Error[] { Errors.NoContactsInCategory });
            _userInput.PressAnyKeyToContinue();
            return null;
        }

        return _contactSelectionView.Render(result.Value);
    }
}
