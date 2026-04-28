using PhoneBook.Application.AddContact;
using PhoneBook.Application.SaveChanges;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;

namespace PhoneBook.ConsoleUI.Services;

internal class AddContactService
{
    private readonly AddContactHandler _addContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;
    private readonly UserInput _userInput;
    private readonly Messages _messages;

    public AddContactService(AddContactHandler addContactHandler, SaveChangesHandler saveChangesHandler,
                                UserInput userInput, Messages messages)
    {
        _addContactHandler = addContactHandler;
        _saveChangesHandler = saveChangesHandler;
        _userInput = userInput;
        _messages = messages;
    }

    internal async Task RunAsync()
    {
        var firstName = _userInput.GetNameFromUser($"Enter your [green]FIRST NAME[/]:");
        var lastName = _userInput.GetNameFromUser($"Enter your [green]LAST NAME[/]:");
        var email = _userInput.GetEmailAddressFromUser();
        var phoneNumber = _userInput.GetPhoneNumberFromUser();

        var addResult = await _addContactHandler.HandleAsync(new(firstName, lastName, phoneNumber, email));

        if (addResult == null)
            _messages.ErrorMessage(new())

        if (addResult.IsFailure)
            _messages.ErrorMessage(addResult.Errors);

        if (addResult.IsSuccess)
            await _saveChangesHandler.HandleAsync();

        _userInput.PressAnyKeyToContinue();
    }
}
