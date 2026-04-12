using PhoneBook.Application.AddContact;
using PhoneBook.Application.SaveChanges;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Services;

internal class AddContactService
{
    private readonly AddContactHandler _addContactHandler;
    private readonly SaveChangesHandler _saveChangesHandler;

    public AddContactService(AddContactHandler addContactHandler, SaveChangesHandler saveChangesHandler)
    {
        _addContactHandler = addContactHandler;
        _saveChangesHandler = saveChangesHandler;
    }

    internal async Task RunAsync()
    {
        var firstName = GetTextFromUser("Please enter your first name:");
        var lastName = GetTextFromUser("Please enter your last name:");
        var email = GetEmailAddressFromUser();
        var phoneNumber = GetPhoneNumberFromUser();

        await _addContactHandler.HandleAsync(new(firstName, lastName, phoneNumber, email));
        await _saveChangesHandler.HandleAsync();


        Console.Write("Press any key to continue...");
        Console.ReadLine();
    }

    private string GetTextFromUser(string message)
    {
        return AnsiConsole.Ask<string>(message);
    }

    private string GetEmailAddressFromUser()
    {
        var emailPrompt = new TextPrompt<string>("What's your [green]email[/]?")
            .Validate(input =>
                input.Contains("@") && input.Contains(".") && input.IndexOf(".") > input.IndexOf("@"),
                "[red]Please enter a valid email address[/]");

        return AnsiConsole.Prompt(emailPrompt);
    }

    private string GetPhoneNumberFromUser()
    {
        var phoneNumberPrompt = new TextPrompt<string>("What's your [green]phone number[/]?")
            .Validate(input =>
                !input.Any(char.IsLetter),
                "[red]Please enter a valid phone number[/]");

        return AnsiConsole.Prompt(phoneNumberPrompt);
    }
}
