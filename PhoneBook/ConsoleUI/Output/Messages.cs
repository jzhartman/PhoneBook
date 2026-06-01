using PhoneBook.Domain.Validation;
using Spectre.Console;

namespace PhoneBook.ConsoleUI.Output;

internal class Messages
{
    internal void ErrorMessage(IEnumerable<Error> errors)
    {
        foreach (var error in errors)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]{error.Code} ERROR:[/] {error.Description}");
        }
    }

    internal void AddCancelledMessage(string name, string addType)
    {
        AnsiConsole.MarkupLineInterpolated($"Cancelled adding [green]{name}[/] to {addType}.");
    }
    internal void AddSucessfulMessage(string name, string addType)
    {
        AnsiConsole.MarkupLineInterpolated($"Successfully added [green]{name}[/] to {addType}");
    }
}
