using Spectre.Console;

namespace PhoneBook.ConsoleUI.Input;

public class UserInput
{
    public string GetTextFromUser(string message)
    {
        return AnsiConsole.Ask<string>(message);
    }

    public string GetEmailAddressFromUser()
    {
        var emailPrompt = new TextPrompt<string>("What's your [green]email[/]?")
            .Validate(input =>
                input.Contains("@") && input.Contains(".") && input.IndexOf(".") > input.IndexOf("@"),
                "[red]Please enter a valid email address[/]");

        return AnsiConsole.Prompt(emailPrompt);
    }

    public string GetPhoneNumberFromUser()
    {
        var phoneNumberPrompt = new TextPrompt<string>("What's your [green]phone number[/]?")
            .Validate(input =>
                !input.Any(char.IsLetter),
                "[red]Please enter a valid phone number[/]");

        return AnsiConsole.Prompt(phoneNumberPrompt);
    }
}
