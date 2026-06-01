using PhoneBook.ConsoleUI.Models;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Input;

internal class UserInput
{
    internal string GetNameFromUser(string message)
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

    internal string GetEmailAddressFromUser()
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

    internal string GetPhoneNumberFromUser()
    {
        var invalidChars = new[] { '-', '(', ')', '+' };

        var phoneNumberPrompt = new TextPrompt<string>("Enter your [green]PHONE NUMBER[/] (7 to 15 digits, '-', '(', ')', and '+' will be ignored):")
            .AllowEmpty()
            .Validate(input =>
            {
                var inputMinusCommonSymbols = new string(input.Where(c => !invalidChars.Contains(c)).ToArray());

                if (inputMinusCommonSymbols.Any(char.IsNumber) == false)
                    return ValidationResult.Error("[red]Please enter a valid phone number[/]");

                if (string.IsNullOrWhiteSpace(inputMinusCommonSymbols))
                    return ValidationResult.Error("[red]Phone number cannot be empty[/]");

                if (inputMinusCommonSymbols.Length < 7 || inputMinusCommonSymbols.Length > 15)
                    return ValidationResult.Error("[red]Please enter a valid phone number[/]");

                return ValidationResult.Success();
            });

        return new string(AnsiConsole.Prompt(phoneNumberPrompt).Where(c => !invalidChars.Contains(c)).ToArray()).Replace(" ", "");
    }

    internal bool GetAddConfirmationFromUser(string name, string addType)
    {
        return AnsiConsole.Confirm($"Confirm adding [green]{name}[/] to the {addType}?");
    }
    internal bool GetDeleteConfirmationFromUser(string name, string deleteType)
    {
        return AnsiConsole.Confirm($"Confirm deleting [green]{name}[/] from the {deleteType}?");
    }
    internal bool GetRenameCategoryConfirmationFromUser(string originalName, string newName)
    {
        return AnsiConsole.Confirm($"Confirm renaming [yellow]{originalName}[/] to [green]{newName}[/]?");
    }
    internal bool GetEditContactConfirmationFromUser(FullContactViewModel originalContact, EditContactViewModel newContact)
    {
        string preamble = $"Confirm the following changes for the contact {originalContact.FirstName} {originalContact.LastName}:";
        string changes = string.Empty;

        if (newContact.ChangedFirstName) changes += $"\t[yellow]{originalContact.FirstName}[/] to [green]{newContact.FirstName}[/]\r\n";
        if (newContact.ChangedLastName) changes += $"\t[yellow]{originalContact.LastName}[/] to [green]{newContact.LastName}[/]\r\n";
        if (newContact.ChangedPhoneNumber) changes += $"\t[yellow]{originalContact.PhoneNumber}[/] to [green]{newContact.PhoneNumber}[/]\r\n";
        if (newContact.ChangedEmail) changes += $"\t[yellow]{originalContact.Email}[/] to [green]{newContact.Email}[/]\r\n";
        if (newContact.ChangedCategory) changes += $"\t[yellow]{originalContact.CategoryName}[/] to [green]{newContact.CategoryName}[/]\r\n";

        return AnsiConsole.Confirm($"{preamble}\r\n\r\n{changes}\r\nConfirm:");
    }

    internal void PressAnyKeyToContinue()
    {
        Console.Write("Press any key to continue...");
        Console.ReadLine();
    }
}
