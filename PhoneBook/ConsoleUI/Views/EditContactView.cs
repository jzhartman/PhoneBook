using PhoneBook.Application.DTOs;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Views;

internal class EditContactView
{
    public void Render(ContactResponse contact)
    {
        AnsiConsole.MarkupInterpolated($"[bold blue]Contact Entry:[/]");

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();

        var table = new Table()
                .HideHeaders()
                .NoBorder();

        table.AddColumn("Key");
        table.AddColumn("Value");

        table.AddRow("[blue]Name:[/]", $"{contact.FirstName} {contact.LastName}");
        table.AddRow("[blue]Phone Number:[/]", $"{contact.PhoneNumber}");
        table.AddRow("[blue]Email:[/]", $"{contact.Email}");

        AnsiConsole.Write(table);
    }
}
