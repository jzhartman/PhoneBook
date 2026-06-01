using Spectre.Console;

namespace PhoneBook.ConsoleUI.Input;

public class UserInput
{
    public string GetNameFromUser(string message)
    {
        var namePrompt = new TextPrompt<string>(message)
            .AllowEmpty()
            .Validate(input =>
            {
                if (string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Error("[red]Name cannot be empty[/]");

                return ValidationResult.Success();
            });

        return AnsiConsole.Prompt(namePrompt);
    }

    public string GetEmailAddressFromUser()
    {
        var emailPrompt = new TextPrompt<string>("Enter your [green]EMAIL[/]:")
            .AllowEmpty()
            .Validate(input =>
            {
                if (input.Contains("@") && input.Contains(".") && input.IndexOf(".") > input.IndexOf("@") && input.Length >= 5)
                    return ValidationResult.Success();

                if (string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Error("[red]Email cannot be empty[/]");

                return ValidationResult.Error("[red]Please enter a valid email address[/]");
            });

        return AnsiConsole.Prompt(emailPrompt);
    }

    public string GetPhoneNumberFromUser()
    {
        var phoneNumberPrompt = new TextPrompt<string>("Enter your [green]PHONE NUMBER[/]:")
            .AllowEmpty()
            .Validate(input =>
            {
                if (input.Any(char.IsLetter))
                    return ValidationResult.Error("[red]Please enter a valid phone number[/]");

                if (string.IsNullOrWhiteSpace(input))
                    return ValidationResult.Error("[red]Phone number cannot be empty[/]");

                if (input.Length < 7 || input.Length > 15)
                    return ValidationResult.Error("[red]Please enter a valid phone number[/]");

                return ValidationResult.Success();
            });

        return AnsiConsole.Prompt(phoneNumberPrompt);
    }

    public bool GetAddConfirmationFromUser(string name, string addType)
    {
        return AnsiConsole.Confirm($"Confirm adding [green]{name}[/] to the {addType}?");

        // Confirm adding {contact name} to contact list?
        // Confirm changing {contact details} to {new contact details}
        // Confirm deleting {contact name} from contact list?
        // Confirm adding {category name} to category list?
        // Confirm renaming {Category Name} to {New Name}
        // Confirm cancelling changes to {contact name}
        // Confirm deleting {category name} from category list
    }

    public void PressAnyKeyToContinue()
    {
        Console.Write("Press any key to continue...");
        Console.ReadLine();
    }
}
