using PhoneBook.Application.AddContact;
using PhoneBook.Application.SaveChanges;
using PhoneBook.ConsoleUI.Input;
using PhoneBook.ConsoleUI.Output;
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
        var firstName = GetNameFromUser("first");
        var lastName = GetNameFromUser("last");
        var email = GetEmailFromUser();
        var phoneNumber = GetPhoneNumberFromUser();

        var addResult = await _addContactHandler.HandleAsync(new(firstName, lastName, phoneNumber, email));

        if (addResult.IsFailure)
            _messages.ErrorMessage(addResult.Errors);

        if (addResult.IsSuccess)
            await _saveChangesHandler.HandleAsync();

        Console.Write("Press any key to continue...");
        Console.ReadLine();
    }

    private string GetNameFromUser(string nameOrder)
    {
        while (true)
        {
            var input = _userInput.GetTextFromUser($"Please enter your {nameOrder} name:");

            if (String.IsNullOrWhiteSpace(input))
            {
                _messages.ErrorMessage(new[] { Errors.EntryNull });
                continue;
            }

            return input;
        }
    }
    private string GetEmailFromUser()
    {
        while (true)
        {
            var input = _userInput.GetEmailAddressFromUser();

            if (String.IsNullOrWhiteSpace(input))
            {
                _messages.ErrorMessage(new[] { Errors.EntryNull });
                continue;
            }
            if (input.Contains('@') && input.Contains('.') && input.IndexOf('@') < input.LastIndexOf('.')
                && input.Count(c => c == '@') == 1)
                return input;

            _messages.ErrorMessage(new[] { Errors.InvalidEmail });
        }
    }
    private string GetPhoneNumberFromUser()
    {
        while (true)
        {
            var input = _userInput.GetPhoneNumberFromUser();

            if (String.IsNullOrWhiteSpace(input))
            {
                _messages.ErrorMessage(new[] { Errors.EntryNull });
                continue;
            }

            if (input.Length >= 7 || input.Length < 15)
                return input;

            _messages.ErrorMessage(new[] { Errors.InvalidEmail });
        }
    }
}
