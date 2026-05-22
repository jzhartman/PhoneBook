using PhoneBook.Application.Contacts.DTOs;
using PhoneBook.Application.Contacts.GetAllContacts;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.ConsoleUI.Views;


namespace PhoneBook.ConsoleUI.Services.Contacts;

internal class ContactSelectionService
{
    private readonly GetAllContactsHandler _getAllContactsHandler;
    private readonly ContactSelectionView _contactSelectionView;
    private readonly Messages _messages;
    private readonly UserInput _userInput;

    public ContactSelectionService(GetAllContactsHandler getAllContactsHandler, ContactSelectionView contactSelectionView,
                                    Messages messages, UserInput userInput)
    {
        _getAllContactsHandler = getAllContactsHandler;
        _contactSelectionView = contactSelectionView;
        _messages = messages;
        _userInput = userInput;
    }

    public async Task<ContactResponse?> RunAsync()
    {
        var result = await _getAllContactsHandler.HandleAsync();

        if (result.IsFailure || result.Value is null)
        {
            _messages.ErrorMessage(result.Errors);
            _userInput.PressAnyKeyToContinue();
            return null;
        }

        return _contactSelectionView.Render(result.Value);
    }
}
