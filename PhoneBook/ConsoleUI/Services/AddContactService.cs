using PhoneBook.Application.AddContact;
using PhoneBook.Application.SaveChanges;
using PhoneBook.ConsoleUI.Input;

namespace PhoneBook.ConsoleUI.Services;

internal class AddContactService
{
    private readonly AddContactHandler _addContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;
    private readonly UserInput _userInput;

    public AddContactService(AddContactHandler addContactHandler, SaveChangesHandler saveChangesHandler, UserInput userInput)
    {
        _addContactHandler = addContactHandler;
        _saveChangesHandler = saveChangesHandler;
        _userInput = userInput;
    }

    internal async Task RunAsync()
    {
        var firstName = _userInput.GetTextFromUser("Please enter your first name:");
        var lastName = _userInput.GetTextFromUser("Please enter your last name:");
        var email = _userInput.GetEmailAddressFromUser();
        var phoneNumber = _userInput.GetPhoneNumberFromUser();

        await _addContactHandler.HandleAsync(new(firstName, lastName, phoneNumber, email));
        await _saveChangesHandler.HandleAsync();

        Console.Write("Press any key to continue...");
        Console.ReadLine();
    }
}
