using PhoneBook.Application.Contacts.AddContact;
using PhoneBook.Application.Contacts.SaveChanges;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
using PhoneBook.Domain.Validation;
using PhoneBook.Domain.Validation.Errors;

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
        var errors = new List<Error>();

        if (addResult.IsSuccess)
        {
            var saveResult = await _saveChangesHandler.HandleAsync();

            if (saveResult is null)
                errors.Add(Errors.SaveResponseNull);

            else if (saveResult.IsFailure)
                errors.AddRange(saveResult.Errors);

            else if (saveResult.IsSuccess)
                return;
        }

        if (addResult.IsFailure)
            errors.AddRange(addResult.Errors);

        if (addResult is null)
            errors.Add(Errors.AddResponseNull);

        _messages.ErrorMessage(errors);
        _userInput.PressAnyKeyToContinue();
    }
}
