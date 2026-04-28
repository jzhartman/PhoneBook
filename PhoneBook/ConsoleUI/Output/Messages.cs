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
}
